import { Component, OnInit } from '@angular/core';
import { MovieService } from 'src/app/core/services/movie.service';
import {ActivatedRoute} from'@angular/router';
import {MovieDetailsModel} from 'src/app/shared/models/movieDetailsModel';
@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movie!: MovieDetailsModel;
  id!: number;
  constructor(private movieService: MovieService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(p=>{
      this.id = +p.get('id')!;
      this.movieService.getMovieDetails(this.id).subscribe(m=>{
        this.movie = m;
        console.log('inside ngOnInit movie-details')
        console.log(this.movie);
      })

    })
  }

}
