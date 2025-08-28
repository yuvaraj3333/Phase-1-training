import { Injectable } from '@angular/core';
import { NavComponent } from 'src/models/header.interface';
import { BookDetails } from 'src/models/book.interface';
import { BookEntry } from 'src/models/bookentry.interface';
import { BankDetails } from 'src/models/bank.interface';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  constructor(private http: HttpClient) {}

  private baseURL = 'http://localhost:5286';

  // return Observable<BookDetails[]>, not raw data
  // getBooks(): Observable<BookDetails[]> {
  //   return this.http.get<BookDetails[]>(`${this.baseURL}/GetBooks`);
  // }

  // token: string = '';
  // header = new HttpHeaders()
  //   .set('Content-Type', 'application/json')
  //   .append('Authorization', `Bearer ${this.token}`);

  // addBook(bookentry: BookEntry): Observable<any> {
  //   return this.http.post(`${this.baseURL}/AddBook`, bookentry, {
  //     headers: this.header,
  //   });
  // }

  getBankDetails(): Observable<BankDetails[]> {
    return this.http.get<BankDetails[]>(`${this.baseURL}/AllCustomers`);
  }

  deleteBankDetail(id: number): Observable<any> {
    return this.http.delete<BankDetails[]>(
      `${this.baseURL}/DeleteCustomer/${id}`
    );
  }
}
