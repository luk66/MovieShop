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
  isFavorited: boolean = false;
  isPurchased: boolean = false;
  isReviewed: boolean = false;
  constructor(private movieService: MovieService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    // ActivatedRoute => that will give us all the information of the current url
    this.activatedRoute.paramMap.subscribe(p=>{
      this.id = +p.get('id')!;
      this.movieService.getMovieDetails(this.id).subscribe(m=>{
        this.movie = m;
        this.isFavorited = m.isFavorited;
        this.isPurchased = m.isPurchased;
        this.isReviewed = m.isReviewed;
        console.log('inside ngOnInit movie-details')
        console.log(this.movie);
      })

    })
  }

}
