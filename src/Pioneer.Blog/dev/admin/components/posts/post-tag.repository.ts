import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Tag } from '../../models/tag';

@Injectable()
export class PostTagRepository {
  private url = '/api/posts';

  constructor(private http: Http) { }
  
  addTag(tagId: number, postId: number): Promise<Tag> {
    return this.http.post(this.url + '\\add-compound', { tagId: tagId, postId: postId })
      .toPromise()
      .then((res: Response) => {
        return res.json() as Tag;
      })
      .catch(this.handleError);
  }

  removeTag(tagId: number, postId: number): Promise<Response> {
    return this.http.post(this.url + '\\delete-compound', { tagId: tagId, postId: postId })
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}