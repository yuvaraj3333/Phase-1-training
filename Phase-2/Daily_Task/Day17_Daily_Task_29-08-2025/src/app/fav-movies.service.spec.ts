import { TestBed } from '@angular/core/testing';

import { FavMoviesService } from './fav-movies.service';

describe('FavMoviesService', () => {
  let service: FavMoviesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FavMoviesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
