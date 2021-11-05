import { Component, OnInit } from '@angular/core';
import { GenreService } from 'src/app/core/services/genre.service';
import {Genre} from 'src/app/shared/models/genre';
@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css']
})
export class GenresComponent implements OnInit {
  movieGenres!: Genre[];
  constructor(private genreService: GenreService) { }

  ngOnInit(): void {
    this.genreService.getAllGenres().subscribe(g => {
      this.movieGenres = g;
      console.log('inside ngOninit of genre component');
      console.table(this.movieGenres);
    })
  }

}
