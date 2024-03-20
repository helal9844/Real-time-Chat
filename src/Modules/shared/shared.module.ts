import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TestErrorsComponent } from './Errors/test-errors/test-errors.component';
import { NotFoundComponent } from './Errors/not-found/not-found.component';
import { RouterModule } from '@angular/router';
import { ServerErrorComponent } from './Errors/server-error/server-error.component';

@NgModule({
  declarations: [TestErrorsComponent, NotFoundComponent, ServerErrorComponent],
  imports: [
    CommonModule,
    RouterModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({ positionClass: 'toast-top-right' }),
  ],
  exports: [BsDropdownModule, ToastrModule],
})
export class SharedModule {}
