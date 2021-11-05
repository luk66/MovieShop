import { Component, OnInit } from '@angular/core';
import { MovieService } from '../../core/services/movie.service';
import { MovieCard } from '../../shared/models/moviecard';
@Component({
  selector: 'app-top-rated-movies',
  templateUrl: './top-rated-movies.component.html',
  styleUrls: ['./top-rated-movies.component.css']
})
export class TopRatedMoviesComponent implements OnInit {

  constructor(private movieService: MovieService) { }
  movieCards!: MovieCard[];
  ngOnInit(): void {
    this.movieService.getTopRatedMovies().subscribe(
      m => {
        this.movieCards = m;
        console.log('inside the ngOnInit method of Home Component');
        console.log(this.movieCards);
      }
    );
  }

}
