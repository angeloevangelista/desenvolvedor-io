import { Component, OnInit } from "@angular/core";
import { Observable, Observer } from "rxjs";

class Usuario {
  nome: string;
  email: string;

  constructor(nome: string, email: string) {
    this.nome = nome;
    this.email = email;
  }
}

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

  usuarioObservable(nome: string, email: string): Observable<Usuario> {
    return new Observable((subscriber) => {
      if (!!nome.length) {
        const usuario = new Usuario(nome, email);

        setTimeout(() => {
          subscriber.next(usuario);
        }, 1000);

        setTimeout(() => {
          subscriber.next(usuario);
        }, 2000);

        setTimeout(() => {
          subscriber.next(usuario);
        }, 3000);

        setTimeout(() => {
          subscriber.next(usuario);
        }, 4000);

        setTimeout(() => {
          subscriber.next(usuario);
          subscriber.complete();
        }, 5000);

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

    // const observer: Observer<string> = {
    //   next: (value) => console.log("Next:", value),
    //   error: (err) => console.error("Error:", err),
    //   complete: () => console.log("Complete"),
    // };

    // const observable = this.minhaObservable("Angelo");
    // observable.subscribe(observer);

    const usuarioObserver: Observer<Usuario> = {
      next: (value) => console.log("Next:", value),
      error: (err) => console.error("Error:", err),
      complete: () => console.log("Complete"),
    };

    const observable = this.usuarioObservable(
      "Angelo",
      "angeloevan.ane@gmail.com"
    );

    const subscription = observable.subscribe(usuarioObserver);

    setTimeout(() => subscription.unsubscribe(), 3000);
  }
}
