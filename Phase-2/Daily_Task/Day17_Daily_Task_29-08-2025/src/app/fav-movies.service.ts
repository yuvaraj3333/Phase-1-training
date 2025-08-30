import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Movie {
  id: number;
  title: string;
  description: string;
  releaseYear: number;
  streamPlatforms: string;
}

@Injectable({
  providedIn: 'root',
})
export class FavMoviesService {
  private baseURL = 'http://localhost:5146';

  constructor(private http: HttpClient) {}

  getMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.baseURL}/GetMovies`);
  }

  deleteMovie(id: number): Observable<string> {
    return this.http.delete(`${this.baseURL}/DeleteMovie/${id}`, {
      responseType: 'text',
    });
  }

  updateMovie(movie: Movie): Observable<string> {
    return this.http.put(`${this.baseURL}/UpdateMovie/${movie.id}`, movie, {
      responseType: 'text',
    });
  }
}
