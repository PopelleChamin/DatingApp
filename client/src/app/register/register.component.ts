import { Component, inject, input, OnInit, output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  cancelRegister = output<boolean>();
  model: any ={};
  registerForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  initializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl(),
      password: new FormControl(),
      confirmPassword: new FormControl()
    })
  }

  register(): void{
    console.log(this.registerForm.value);
    //this.accountService.register(this.model).subscribe({
    //  next: (response) =>{
    //    console.log(response),
    //    this.cancel();
    //  },
    //  error: (error) => {
    //    this.toastr.error(error.errors);
    //  }
    //});
  }

  cancel(): void{
    this.cancelRegister.emit(false);
  }
}
