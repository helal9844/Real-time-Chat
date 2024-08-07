import { Component, OnInit } from '@angular/core';
import { Member } from 'src/Modules/shared/Interfaces/member';
import { UsersService } from 'src/Modules/shared/Services/users.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  members: Member[];
  constructor(private usersService: UsersService) {}
  ngOnInit(): void {
    this.loadMembers();
  }
  loadMembers() {
    this.usersService.getUsers().subscribe((response) => {
      this.members = response;
    });
  }
}
