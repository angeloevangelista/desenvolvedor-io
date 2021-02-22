import { Component, OnInit } from "@angular/core";
import { Observable, Observer } from "rxjs";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styles: [],
})
export class AppComponent implements OnInit {
  title = "RXJS";

  minhaPromise(nome: string): Promise<string> {
    return new Promise((resolve, reject) => {
      if (nome.length > 6) {
        return setTimeout(() => {
          resolve("Seja bem vindo");
        }, 1000);
      }

      return reject("Quantidade de caracteres inválida.");
    });
  }

  minhaObservable(nome: string): Observable<string> {
    return new Observable((subscriber) => {
      if (!!nome.length) {
        subscriber.next(`Olá, ${nome}!`);

        setTimeout(() => {
          subscriber.next(`Ainda por aqui, ${nome}?`);
          subscriber.complete();
        }, 1000);

        return;
      }

      subscriber.error("Opa, preciso saber o seu nome, jovem :/");
    });
  }

  ngOnInit(): void {
    // this.minhaPromise("Angelão")
    //   .then((response) => console.log(response))
    //   .catch((error) => console.error(error));

    // this.minhaObservable("").subscribe(
    //   (result) => console.log(result),
    //   (error) => console.error(error)
    // );

    const observer: Observer<string> = {
      next: (value) => console.log("Next:", value),
      error: (err) => console.error("Error:", err),
      complete: () => console.log("Complete"),
    };

    const observable = this.minhaObservable("Angelo");

    observable.subscribe(observer);
  }
}
