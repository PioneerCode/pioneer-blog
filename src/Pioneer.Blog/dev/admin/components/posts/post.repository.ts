import { Injectable }               from '@angular/core';
import { Headers, Http, Response }  from '@angular/http';
import { Post }                     from '../../models/post';
import {Observable}                 from "rxjs/Rx";

import 'rxjs/add/operator/toPromise';

@Injectable()
export class PostRepository {
  private postUrl = '/api/posts';

  constructor(private http: Http) { }

  getAll(): Promise<Post[]> {
    return this.http.get(this.postUrl)
      .toPromise()
      .then(res => {
        const body = res.json();
        return body || [];
      })
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}