import { Component, OnInit } from '@angular/core';
import { FavMoviesService, Movie } from '../fav-movies.service';

@Component({
  selector: 'app-fav-movies',
  templateUrl: './fav-movies.component.html',
  styleUrls: ['./fav-movies.component.css'],
})
export class FavMoviesComponent implements OnInit {
  movies: Movie[] = [];
  // isLoading = true;
  error: string | null = null;

  // form model
  updateMovieData: Movie = {
    id: 0,
    title: '',
    description: '',
    releaseYear: new Date().getFullYear(),
    streamPlatforms: '',
  };

  constructor(private favMoviesService: FavMoviesService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies(): void {
    this.favMoviesService.getMovies().subscribe({
      next: (data) => {
        this.movies = data;
        // this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Failed to load movies';
        console.error(err);
        // this.isLoading = false;
      },
    });
  }

  deleteMovie(id: number): void {
    if (!confirm('Are you sure you want to delete this movie?')) return;

    this.favMoviesService.deleteMovie(id).subscribe({
      next: (msg) => {
        console.log(msg);
        this.movies = this.movies.filter((m) => m.id !== id);
      },
      error: (err) => {
        console.error('Failed to delete movie', err);
        this.error = 'Failed to delete movie';
      },
    });
  }

  updateMovie(): void {
    this.favMoviesService.updateMovie(this.updateMovieData).subscribe({
      next: (msg) => {
        console.log(msg);
        alert('Movie updated successfully!');

        // Refresh list
        this.loadMovies();

        // Reset form
        this.updateMovieData = {
          id: 0,
          title: '',
          description: '',
          releaseYear: new Date().getFullYear(),
          streamPlatforms: '',
        };
      },
      error: (err) => {
        console.error('Failed to update movie', err);
        this.error = 'Failed to update movie';
      },
    });
  }
}
