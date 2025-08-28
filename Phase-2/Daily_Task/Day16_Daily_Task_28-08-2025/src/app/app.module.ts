import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { XyzComponent } from './xyz/xyz.component';

import { FormsModule } from '@angular/forms';
import { BookService } from './book.service';
import { HttpClientModule } from '@angular/common/http';
import { BankComponent } from './bank/bank.component';

@NgModule({
  declarations: [AppComponent, XyzComponent, BankComponent],
  imports: [BrowserModule, FormsModule, HttpClientModule],
  providers: [BookService],
  bootstrap: [AppComponent],
})
export class AppModule {}
