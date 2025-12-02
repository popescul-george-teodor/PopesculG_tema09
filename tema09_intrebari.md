# Tema acasa Laborator 10

1. Utilizați pentru texturare imagini cu transparență și fără. Ce observați?

Pentru imaginile transparente, se observă că pixelii transparenți preiau culoarea obiectului (alb spre exemplu). Pentru a avea transparență la nivel de obiect trebuie să realizăm setări adiționale.

2. Ce formate de imagine pot fi aplicate în procesul de texturare în OpenGL?

Se pot folosi formate pentru imagini 1D, 2D și nu numai, cum ar fi .png, .bmp, .gif ș.a.m.d. .

3. Specificați ce se întâmplă atunci când se modifică culoarea (prin manipularea canalelor RGB) obiectului texturat.

Textura ia o tentă in funcție de culoarea aplicată. Spre exemplu, pentru roșu, imaginea devine roșiatică. Doar pentru alb imaginea apare fără distorsiuni.

4. Ce deosebiri există între scena ce utilizează obiecte texturate în modul iluminare activat, respectiv dezactivat?

În lipsa iluminării obiectele vor apărea întunecate, dar încă poate fi posibil să discerni textura de pe obiect. Textura nu va afecta iluminarea direct, decât dacă se folosesc texturi specifice iluminării (normal maps, metallic maps, etc.).
