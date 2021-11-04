import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
// specify all the routes required by the augular applications
const routes: Routes = [
  // path route for my home page http://localhost:4200/
  {path: "", component: HomeComponent},
  // {path: "movie/:id", component: MovieDetailComponent},
  // {path:"admin/createmovie", component: CreateMovieComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
