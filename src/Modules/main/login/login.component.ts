import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/Modules/shared/Services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  ngOnInit(): void {}
  model: any = {};

  @Output() cancelLogin = new EventEmitter();
  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  Login() {
    this.accountService.login(this.model).subscribe((result) => {
      this.router.navigateByUrl('/members');
    });
  }
  register() {
    this.accountService.register(this.model).subscribe(
      (result) => {
        console.log(result);
        this.cancel();
      },
      (error) => {
        console.log(error);
        this.toastr.error(error.error);
      }
    );
  }
  cancel() {
    this.cancelLogin.emit(false);
  }
}
