import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookDetails } from 'src/models/book.interface';
import { BookEntry } from 'src/models/bookentry.interface';
import { BankDetails } from 'src/models/bank.interface';

// ✅ Move interface outside
interface UserLogin {
  username: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private baseURL = 'http://localhost:5286';

  constructor(private http: HttpClient) {}

  // ✅ Return observable instead of subscribing here
  performLoginAndGetToken(): Observable<string> {
    const login: UserLogin = { username: 'sam', password: 'admin@123' };
    return this.http.post(`${this.baseURL}/Login`, login, {
      responseType: 'text',
    });
  }

  // ✅ Example with Authorization header
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token') ?? '';
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });
  }

  // getBooks(): Observable<BookDetails[]> {
  //   return this.http.get<BookDetails[]>(`${this.baseURL}/GetBooks`, {
  //     headers: this.getAuthHeaders(),
  //   });
  // }

  // addBook(bookentry: BookEntry): Observable<any> {
  //   return this.http.post(`${this.baseURL}/AddBook`, bookentry, {
  //     headers: this.getAuthHeaders(),
  //   });
  // }

  getBankDetails(): Observable<BankDetails[]> {
    return this.http.get<BankDetails[]>(`${this.baseURL}/AllCustomers`, {
      headers: this.getAuthHeaders(),
    });
  }

  deleteBankDetail(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/DeleteCustomer/${id}`, {
      headers: this.getAuthHeaders(),
    });
  }
}
