import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Tag } from '../models/tag';
import { environment } from '../../environments/environment';

@Injectable()
export class PostTagRepository {
  private url = environment.apiUrl + '/api/post-tags';

  constructor(private http: HttpClient) { }

  add(tagId: number, postId: number): Promise<Tag> {
    return this.http.post(this.url, { tagId: tagId, postId: postId })
      .toPromise()
      .then((res: Tag) => {
        return res as Tag;
      })
      .catch(this.handleError);
  }

  removeByCompound(tagId: number, postId: number): Promise<Response> {
    return this.http.post(this.url + '/remove/compound', { tagId: tagId, postId: postId })
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
