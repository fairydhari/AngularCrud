import { Component, OnInit } from '@angular/core';
import { UserDetailsService } from '../../services/user-details.service';
//import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  constructor(private userDetailsService: UserDetailsService){}//, private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();
  }
  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.userDetailsService.selectedUser = {
      userId: null,
      email: '',
      username: '',
      password: '',
      mobilephone: ''
     
    }
    
  }
  onSubmit(form: NgForm) {
    if (form.value.userId == null) {
      this.userDetailsService.postUserDetails(form.value)
        .subscribe(data => {
          this.resetForm(form);
          this.userDetailsService.getUserList();
         // this.toastr.success('New Record Added Succcessfully', 'user Register');
        })
    }
    else {
      this.userDetailsService.putUserDetails(form.value.userId, form.value)
      .subscribe(data => {
        this.resetForm(form);
        this.userDetailsService.getUserList();
       // this.toastr.info('Record Updated Successfully!', 'user Register');
      });
    }
  }
}
