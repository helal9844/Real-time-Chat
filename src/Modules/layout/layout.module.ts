import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { MainModule } from '../main/main.module';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
@NgModule({
  declarations: [HeaderComponent],
  imports: [CommonModule, MainModule, RouterModule, BsDropdownModule.forRoot()],
  exports: [HeaderComponent],
})
export class LayoutModule {}
