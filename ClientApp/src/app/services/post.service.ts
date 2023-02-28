import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from '../models/model.js';
@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getPostsBySearchCriteria(searchTags : string, sortBy: string, direction:string): Observable<Post[]> {
    let apiPath =  this.getSubmitBaseUrl(searchTags, sortBy, direction);
    return this.http.get<Post[]>(apiPath);
  }

  private getSubmitBaseUrl(searchTags : string, sortBy: string, direction:string ): string {
    return this.baseUrl + `api/Post/search?tags=${searchTags}&&sortBy=${sortBy}&&direction=${direction}`;
  }
}
