import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';

import {sortByList, directionList} from "../../_data/data.js"
import { PostService } from '../../services/post.service.js';
import { Post } from '../../models/model.js';
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
  isLoading = false;
  isError = false;

  constructor(private postService: PostService) {
  }

  ngOnInit() : void{
    this.searchForm = new FormGroup({
      tags: new FormControl('',[Validators.required]),
      sortBy: new FormControl('id'),
      direction: new FormControl('asc'),
    });
  }

  onSubmit() {
    let searchTags = this.searchForm.value.tags;
    let sortBy = this.searchForm.value.sortBy;
    let direction = this.searchForm.value.direction;
    this.isLoading = true;
    this.isError = false;

    this.postService.getPostsBySearchCriteria(searchTags, sortBy, direction).subscribe(
      (data: Post[]) => {
        this.posts = data;
      }, err => {
        this.isLoading = false;
        this.isError = true;
      }, () => {
        this.isLoading = false;
        this.isError = false;
      }
    )
  }
}
