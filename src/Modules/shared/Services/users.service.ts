import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environments';
import { Member } from '../Interfaces/member';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private url: any = environment.baseUrl;
  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<Member[]>(this.url + 'User');
  }
  getUser(userName: string) {
    return this.http.get<Member>(this.url + 'User/' + userName);
  }
}
