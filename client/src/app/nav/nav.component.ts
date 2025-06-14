import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule,RouterLink,RouterLinkActive,TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountservice=inject(AccountService)
  private router=inject(Router);
  private toatr= inject(ToastrService)
  model:any={};
 login(){
  this.accountservice.login(this.model).subscribe(
    {
      next:response=>{
      this.router.navigateByUrl("/members")
    },
    error:error=>this.toatr.error(error.error)
    }
  );
  }
    logout(){
      this.accountservice.logout();
    this.router.navigateByUrl("/")
  }
}

