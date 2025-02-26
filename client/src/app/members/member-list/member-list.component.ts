import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from '../member-card/member-card.component';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  memberServices = inject(MembersService);
  pageNumber = 1;
  pageSize = 5;

  ngOnInit(): void {
    if (!this.memberServices.paginatedresult()){
      this.loadMembers();
    }
  }

  loadMembers(){
    this.memberServices.getMembers(this.pageNumber, this.pageSize);
  }
}
