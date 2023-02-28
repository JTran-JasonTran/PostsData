import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';

import {sortByList, directionList} from "../_data/data.js"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  posts: Post[];
  sortByList = sortByList;
  directionList = directionList;
  searchForm:FormGroup;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() : void{
    this.searchForm = new FormGroup({
      tags: new FormControl('',[Validators.required]),
      sortBy: new FormControl('id'),
      direction: new FormControl('asc'),
    });
  }

  onSubmit() {
    let url =  this.getSubmitBaseUrl();
    this.http.get<Post[]>(url).subscribe(result => {
      this.posts = result;
      console.log(result);
    }, error => console.error(error));
  }

  private getSubmitBaseUrl(): string {
    let searchTags = this.searchForm.value.tags;
    let sortBy = this.searchForm.value.sortBy;
    let direction = this.searchForm.value.direction;

    return this.baseUrl + `api/Post/search?tags=${searchTags}&&sortBy=${sortBy}&&direction=${direction}`;
  }
}

interface Post {
  author: string;
  authorId: number;
  id: number;
  likes: number;
  popularity: number;
  reads: number;
  tags: Array<string>;
}

