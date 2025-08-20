from fastapi import FastAPI, Depends, HTTPException, Response
from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.orm import sessionmaker, Session, declarative_base
from pydantic import BaseModel, ConfigDict
from typing import List
import uvicorn
from fastapi.middleware.cors import CORSMiddleware
from dotenv import load_dotenv
import os

load_dotenv()

app = FastAPI()

origins = ["http://localhost", "http://localhost:8000"]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# DB Setup

DB_URL = os.getenv("DB_URL")

engine = create_engine(DB_URL, connect_args={"check_same_thread": False})
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base = declarative_base()

class AlbumClass(Base):
    __tablename__ = 'album'
    id = Column(Integer, primary_key=True, index=True, autoincrement=True)
    title = Column(String(50), nullable=False)
    artist = Column(String(50), nullable=False)
    year = Column(Integer, nullable=False)
    duration = Column(Integer, nullable=False)

Base.metadata.create_all(bind=engine)

# Pydantic Schemas

class AlbumCreate(BaseModel):
    title: str
    artist: str
    year: int
    duration: int

class AlbumRead(BaseModel):
    id: int
    title: str
    artist: str
    year: int
    duration: int
    model_config = ConfigDict(from_attributes=True)

class AlbumDTO(BaseModel):
    title: str
    model_config = ConfigDict(from_attributes=True)

# FastAPI Setup

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()


# @app.post("/add", response_model=AlbumRead)
# def add_album(album_in: AlbumCreate, db: Session = Depends(get_db)):
#     album = AlbumClass(
#         title=album_in.title,
#         artist=album_in.artist,
#         year=album_in.year,
#         duration=album_in.duration
#     )
#     db.add(album)
#     db.commit()
#     db.refresh(album)
#     return album

# POST: /{artist}/add
@app.post("/{artist}/add", response_model=AlbumRead)
def add_album_with_existing_artist_permission(
    artist: str,
    album_in: AlbumCreate,
    db: Session = Depends(get_db)
):
    existing_artist = db.query(AlbumClass).filter_by(artist=artist).first()
    if not existing_artist:
        raise HTTPException(
            status_code=404,
            detail=f"Artist '{artist}' not found in database"
        )

    album = AlbumClass(
        title=album_in.title,
        artist=album_in.artist,
        year=album_in.year,
        duration=album_in.duration
    )
    db.add(album)
    db.commit()
    db.refresh(album)
    return album


# GET: /all
@app.get("/all", response_model=List[AlbumRead])
async def get_albums(db: Session = Depends(get_db)):
    return db.query(AlbumClass).all()

# GET: /getBy
@app.get("/getBy/{id}", response_model=AlbumDTO)
async def get_by(id: int, db: Session = Depends(get_db)):
    alb = db.query(AlbumClass).filter_by(id=id).first()
    if not alb:
        return {"title": None}
    return alb

async def ValidateExistence(id: int, dp: Session = Depends(get_db),):
    alb = dp.query(AlbumClass).filter_by(id=id).first()
    if alb is None:
        return 0
    else:
        return id

# DELETE: /delete
@app.delete("/delete/{id}")
async def delete_album(id: int, db: Session = Depends(get_db), validate_id: int = Depends(ValidateExistence)):
    if validate_id == 0:
        raise HTTPException(status_code=404, detail="Album not found", headers={"X-Error": "Validated by me"})
    st = db.query(AlbumClass).filter_by(id=id).first()
    db.delete(st)
    db.commit()
    return {"status": "deleted"}

# GET: /redirect
from fastapi.responses import RedirectResponse
@app.get("/redirect", response_class=RedirectResponse)
async def redirect():
    return RedirectResponse("https://www.payoda.com/")

# from fastapi.responses import FileResponse
# path = ""
# @app.get("/download", response_class=FileResponse)
# async def download_image():
#     res = FileResponse(path)
#     res.set_cookie(key="is_downloaded", value="true")
#     return res

# from fastapi.responses import JSONResponse
# @app.get("/json", response_class=JSONResponse)
# async def json():
#     content = {"item_id": "Foo"}
#     headers = {"X-Web-Framework": "My fast api", "Content-Language": "eng-US"}
#     res = JSONResponse(content=content, headers=headers)
#     return res

# OAuth

# from fastapi.security import OAuth2PasswordRequestForm
# from fastapi.security import OAuth2PasswordBearer
# from typing import Annotated, Union
# from datetime import datetime, timedelta, timezone
# import jwt

# SECRETE_KEY = "aaabbbcccddd"
# ALGORITHM = "HS256"

# def create_access_token(data: dict):
#     encode = data.copy()
#     expire = datetime.now(timezone.utc) + timedelta(minutes=60)
#     encode.update({"expire": expire.strftime("%Y-%m-%d %H:%H:%S")})
#     encoded_jwt = jwt.encode(encode, SECRETE_KEY, ALGORITHM)
#     return encoded_jwt


# @app.post("/login")
# async def login(form_data: Annotated[OAuth2PasswordRequestForm, Depends()]):
#     token = create_access_token(data={"user_id": "atom"})
#     return {"access_token": token, "token_type": "bearer"}






# oauth2_scheme = OAuth2PasswordBearer(tokenUrl='login')

# async def validate_token(token: Annotated[str, Depends(oauth2_scheme)]):
#     credential_exception = HTTPException(status_code = status.HTTP_401_UNAUTHORIZED, details="Invalid Creds", headers={"WWW-Authenticate": "Bearer"})
    
#     payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
    
#     username = payload.get("user")
#     if username is None:
#         raise credential_exception
#     print("user: ", username)
#     return username




# @app.get("/getName")
# async def getName(my_name: str, db: Session = Depends(get_db), usr = Depends(validate_token)):
#     st = db.query(title).filter_by(Name=my_name).first()
#     return st

# import logging 
# from logging.handlers import TimeRotatingFileHandler

# logger = logging.getLogger(__name__)
# file_handler = logging. FileHandler("my_log.log")
# time = TimeRotatingFileHandler("time.log", when='midnight', backupCount=10)
# formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
# file_handler.setFormatter(formatter)
# logger.addHandler(file_handler)
# logger.setLevel(logging.INFO)

@app.middleware("http")
async def addmiddleware(request, call_next):
    print("middleware has intercepted the call")
    response = await call_next(request)
    return response

if __name__ == "__main__":
    uvicorn.run("app:app", host="127.0.0.1", port=8000, reload=True)