import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_modules/Member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
private memberService= inject(MembersService);
members:Member[]=[];
  ngOnInit(): void {
    this.loadMembers();
  
}
loadMembers()
{
  console.log('test');
this.memberService.getMembers().subscribe({
  next:members=>this.members=members,
})
}
}
