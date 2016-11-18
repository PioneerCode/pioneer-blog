import { Injectable }               from '@angular/core';
import { Headers, Http, Response }  from '@angular/http';
import { Tag }                     from '../../models/tag';
import {Observable}                 from "rxjs/Rx";

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TagRepository {
  private tagUrl = '/api/tags';

  constructor(private http: Http) { }

  get(id: number, includeExcerpt = false): Promise<Tag> {
    return this.http.get(this.tagUrl + '/' + id)
      .toPromise()
      .then(res => {
        const body = res.json();
        return body || {} as Tag;
      })
      .catch(this.handleError);
  }

  getAll(): Promise<Tag[]> {
    return this.http.get(this.tagUrl)
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