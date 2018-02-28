import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { IToken } from '../models/user';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';

const API_URL = environment.apiUrl;

export interface ILoginRequest {
  email: string;
  password: string;
}

@Injectable()
export class UserRepository {

  constructor(private http: HttpClient) { }

  login(loginModel: ILoginRequest): Observable<IToken> {
    return this.http.post(`${API_URL}/api/accounts/token`, loginModel)
      .map((resp: IToken) => {
        return resp || {} as IToken;
      })
      .catch(this.handleError);
  }

  private handleError(error: Response | any) {
    console.error('UserRepository::handleError', error);
    return Observable.throw(error);
  }
}
