import { Component, OnInit } from "@angular/core";

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

  ngOnInit(): void {
    this.minhaPromise("Angelão")
      .then((response) => console.log(response))
      .catch((error) => console.error(error));
  }
}
