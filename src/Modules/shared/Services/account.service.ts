import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environments';
import { User } from '../Interfaces/user';
import { ReplaySubject } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private url: any = environment.baseUrl;
  private currentUserSrc = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSrc.asObservable();
  constructor(private http: HttpClient) {}
  login(model: any) {
    return this.http.post(this.url + 'Credentials/Login', model).pipe(
      map((result: User) => {
        const user = result;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSrc.next(user);
        }
      })
    );
  }
  register(model: any) {
    return this.http.post(this.url + 'Credentials/Register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSrc.next(user);
        }
      })
    );
  }
  setCurrentUser(user: User) {
    this.currentUserSrc.next(user);
  }
  logOut() {
    localStorage.removeItem('user');
    this.currentUserSrc.next(null);
  }
}
