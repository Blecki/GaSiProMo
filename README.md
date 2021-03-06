November 4th, 2015

The opening scene is begining to take shape. I've also ditched the console interface for something I will have a little more control of - a simple webpage. It's not really a webpage, because running this in a browser is currently a no-go. It's a WPF application that is nothing but a webbrowser control. It's a far nicer experience than the console.

I don't think I'm moving at a pace that will see me through to completion of the story by the end of the month, but hey, I've only had to dive into the underlying system to make it do what I want literally dozens of times.


November 1st, 2015

I'm writing Interactive Fiction for GaSiProMo. What is Interactive Fiction? Read IntroductionToIF.pdf for a very nice run down and introduction to the core concepts of Interactive Fiction. If you're a new IF author, Inform7 is a fantastic tool for creating IF. I became quite an expert in Inform7, and I fell in love with the way it modelled the world, and it's rule-based programming paradigm. But, there are some things about Inform7 I really, really hate. First among them is the language itself. Inform7 tries to be natural English, but English is such an irregular language that Inform7 never really stood a chance. The result is something with the verbosity of written language, and all the little highly particular syntax rules of a programming language. (Also, all variables are global. This is a really big one for me.) Second, Inform7 is tied to a very specific virtual machine. That's all it can compile to. This is a very ancient machine, by all accounts, and it is constantly throwing barriers in front of the author. Your game is too large, it says. You have too much detail. You have too many rules. 

Instead of fighting against a language to make it do things the creators never imagined, instead of fighting an ancient virtual machine that really isn't up to the task anymore, I decided to take what I liked about the Inform7 system and reimplement it in C#. The result is my IF authoring system, RMUD (Named such because it is, actually, a multiplayer mud engine that just happens to work for singleplayer games as well). You can find it here https://github.com/Blecki/RMUD 

The system works almost identically to Inform7. Actions invoke rules, which have logic and effects. The syntax, however, is wildly different. It has not been easy to mold C# around some of these concepts. Ironically, for simple games, the RMUD version is far more verbose. You can see some of the differences in implementation (and the warts) by digging through the Cloak of Darkness sample at https://github.com/Blecki/RMUD/tree/master/SinglePlayer 

I've included a CloakOfDarkness binary in the repository if you want to check it out. It's a very simple game.

I'm going to commit a cardinal gitsin and include build files in my repo. You don't need to build to play.. you just need the contents of the Out directory.

