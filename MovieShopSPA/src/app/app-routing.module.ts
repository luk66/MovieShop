import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import {MovieDetailsComponent} from './movies/movie-details/movie-details.component';
import { TopRatedMoviesComponent } from './movies/top-rated-movies/top-rated-movies.component';
import {GenresComponent} from 'src/app/genres/genres/genres.component'
// specify all the routes required by the augular applications
const routes: Routes = [
  // path route for my home page http://localhost:4200/
  {path: "", component: HomeComponent},
  {path: "movies/toprated", component: TopRatedMoviesComponent},
  {path: "movies/:id", component: MovieDetailsComponent},
  
  {path: "genres", component: GenresComponent},
  // {path:"admin/createmovie", component: CreateMovieComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
