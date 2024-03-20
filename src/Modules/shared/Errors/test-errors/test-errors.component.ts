import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environments';
@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css'],
})
export class TestErrorsComponent implements OnInit {
  private url: any = environment.baseUrl;
  validationError: string[] = [];
  constructor(private http: HttpClient) {}
  ngOnInit(): void {}
  get404Error() {
    this.http.get(this.url + 'Error/not-found').subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  get400Error() {
    this.http.get(this.url + 'Error/bad-request').subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  get500Error() {
    this.http.get(this.url + 'Error/server-error').subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  get401Error() {
    this.http.get(this.url + 'Error/Auth').subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  get400ValidationError() {
    this.http.post(this.url + 'Credentials/Register', {}).subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
        this.validationError = error;
      }
    );
  }
}
