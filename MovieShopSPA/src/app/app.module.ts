import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';
// import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { GenresComponent } from './genres/genres/genres.component';
// import { TopRatedMoviesComponent } from './movies/top-rated-movies/top-rated-movies.component';
// import { MoviesModule } from './movies/movies.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    MovieCardComponent,
    GenresComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    // MoviesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
