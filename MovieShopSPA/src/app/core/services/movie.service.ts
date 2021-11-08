import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {MovieCard} from 'src/app/shared/models/moviecard';
import {MovieDetailsModel} from 'src/app/shared/models/movieDetailsModel';
import {HttpClient} from '@angular/common/http';
import {environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  // injection
  constructor(private http:HttpClient) { }
  // https://localhost:5001/api/Movies/toprevenue
  // many methods that will beused by components
  // HomeComponent will call this function
  getTopRevenueMovies(): Observable<MovieCard[]>{
    // call our API, using HttpClient (XMLHttpRequest) to make GET request 
    // HttpClient class comes from HttpClientModule (Angular team created)
    return this.http.get<MovieCard[]>(`${environment.apiBaseURl}Movies/toprevenue`);
  }

  getTopRatedMovies(): Observable<MovieCard[]> {
    return this.http.get<MovieCard[]>(`${environment.apiBaseURl}Movies/toprated`);
  }

  getMovieDetails(id: number): Observable<MovieDetailsModel>{
    
    return this.http.get<MovieDetailsModel>(`${environment.apiBaseURl}Movies/${id}`);
  }
}
