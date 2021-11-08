import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MoviesComponent } from './movies.component';
import {TopRatedMoviesComponent} from './top-rated-movies/top-rated-movies.component';
import {CastDetailsComponent} from './cast-details/cast-details.component';
const routes: Routes = [
  {path: '', component:MoviesComponent,
   children: [
    {path: 'toprated', component: TopRatedMoviesComponent},
    {path: ':id', component:MovieDetailsComponent},  
    {path: 'cast/:id', component: CastDetailsComponent},
   ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MoviesRoutingModule { }
