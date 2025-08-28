import { Component } from '@angular/core';
import { NavComponent } from '../../models/header.interface';
import { BookDetails } from 'src/models/book.interface';
import { BookEntry } from 'src/models/bookentry.interface';
import { BankDetails } from 'src/models/bank.interface';
import { BookService } from '../book.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-xyz',
  templateUrl: './xyz.component.html',
  styleUrls: ['./xyz.component.css'],
})
export class XyzComponent {
  constructor(private bookService: BookService) {}

  logoList: NavComponent[] = [];
  bookList: BookDetails[] = [];
  bankList: BankDetails[] = [];

  // getBookDetails() {
  //   this.bookList = [];
  // }

  // add_title: string = '';
  // add_description: string = '';
  // add_rating: number = 0;
  // add_price: number = 0;

  // addMore() {
  //   console.log('Button Clicked');

  //   this.bookService
  //     .addBook({
  //       title: this.add_title,
  //       description: this.add_description,
  //       rating: this.add_rating,
  //       price: this.add_price,
  //     })
  //     .subscribe(
  //       (res) => {
  //         console.log('Book added successfully:', res);

  //         // Refresh the list from API
  //         // this.bookService.getBooks().subscribe((data) => {
  //         //   this.bookList = data;
  //         // });
  //         this.fetchAPI();

  //         // Clear input fields
  //         this.add_title = '';
  //         this.add_description = '';
  //         this.add_rating = 0;
  //         this.add_price = 0;
  //       },
  //       (err) => {
  //         console.error('Error adding book:', err);
  //       }
  //     );
  // }

  // fetchAPI() {
  //   this.bookService.getBooks().subscribe((data) => {
  //     this.bookList = data;
  //   });
  // }

  ngOnInit() {
    console.log('Fetching bank details...');
    this.getBankDetails();
  }

  getBankDetails() {
    this.bookService.getBankDetails().subscribe({
      next: (data) => {
        this.bankList = data;
        console.log('Bank Data:', data);
      },
      error: (err) => console.error('Error fetching bank details:', err),
    });
  }

  deleteId: number | null = null;

  deleteCustomer() {
    if (!this.deleteId) {
      console.warn('Please enter a valid ID');
      return;
    }

    this.bookService.deleteBankDetail(this.deleteId).subscribe({
      next: (res) => {
        console.log('Customer deleted successfully:', res);

        // Refresh list
        this.getBankDetails();

        // Clear input
        this.deleteId = null;
      },
      error: (err) => console.error('Error deleting customer:', err),
    });
  }
}
