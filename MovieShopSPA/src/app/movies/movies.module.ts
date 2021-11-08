import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MoviesRoutingModule } from './movies-routing.module';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { MovieDetailsComponent} from './movie-details/movie-details.component';
import {TopRatedMoviesComponent} from './top-rated-movies/top-rated-movies.component';
import { MoviesComponent } from './movies.component';

@NgModule({
  declarations: [
    CastDetailsComponent,
    TopRatedMoviesComponent,
    MovieDetailsComponent,
    MoviesComponent,
  ],
  imports: [
    CommonModule,
    MoviesRoutingModule
  ]
})
export class MoviesModule { }
