from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import List
from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.orm import sessionmaker, declarative_base

DATABASE_URL = "sqlite:///./songs.db"
engine = create_engine(DATABASE_URL, connect_args={"check_same_thread": False})
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base = declarative_base()

class Song(Base):
    __tablename__ = "songs"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String, nullable=False)
    playing_time = Column(Integer, nullable=False)  # in seconds
    album = Column(String, nullable=False)

Base.metadata.create_all(bind=engine)

class SongCreate(BaseModel):
    name: str
    playing_time: int
    album: str

class SongResponse(SongCreate):
    id: int

    class Config:
        from_attributes = True   

app = FastAPI(title="Song API with FastAPI + SQLite")


def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()


@app.post("/songs/", response_model=SongResponse)
def create_song(song: SongCreate):
    db = SessionLocal()
    new_song = Song(name=song.name, playing_time=song.playing_time, album=song.album)
    db.add(new_song)
    db.commit()
    db.refresh(new_song)
    db.close()
    return new_song


@app.get("/songs/", response_model=List[SongResponse])
def get_all_songs():
    db = SessionLocal()
    songs = db.query(Song).all()
    db.close()
    return songs


@app.get("/songs/{song_id}", response_model=SongResponse)
def get_song(song_id: int):
    db = SessionLocal()
    song = db.query(Song).filter(Song.id == song_id).first()
    db.close()
    if not song:
        raise HTTPException(status_code=404, detail="Song not found")
    return song
