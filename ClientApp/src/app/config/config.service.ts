import
{ Injectable }
from '@angular/core';
import
{ HttpClient, HttpResponse }
from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';


@Injectable()
export class ConfigService
{
  configUrl = 'assets/config.json';
  constructor(private http: HttpClient) { }

  getConfig() {
    return this.http.get<Config>(this.configUrl);
  }

  getConfigResponse(): Observable<HttpResponse<Config>> {
    return this.http.get<Config>(
      this.configUrl, { observe: 'response' });
  }
}
interface Config {
  heroesUrl: string;
  textfile: string;
  date: any;
}