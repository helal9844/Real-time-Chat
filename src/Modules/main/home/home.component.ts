import { Component, OnInit } from '@angular/core';
import { User } from 'src/Modules/shared/Interfaces/user';
import { UsersService } from 'src/Modules/shared/Services/users.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  ngOnInit(): void {}
  constructor(private usersService: UsersService) {}
  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
