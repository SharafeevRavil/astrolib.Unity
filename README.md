# astrolib.Unity
astolib.Unity - интерактивное приложение Unity для просмотра звездного неба, использующее C++ библиотеку [astrolib](https://github.com/IldarS2000/astrolib) для вычисления характеристик звезд.

Приложение имеет возможность просматривать звездное небо, просматривать характеристики выбранных звезд, включать отображение созвездий и сетки right ascension/declination, сравнивать яркости звезд, искать звезды по названию/номеру каталога.
## Структура репозитория
Репозиторий состоит из двух частей:
1) astrolib-my - обертка над библиотекой astrolib для ее дальнейшей сборки в DLL и последующего использования в .NET посредством P/Invoke
2) AstrolibUI - приложение Unity, использующее сборку astrolib-my для произведения вычислений над характеристиками звезд

## Исходный датасет
Приложение использует каталог звезд [Yale Bright Star Catalog](http://tdc-www.harvard.edu/catalogs/bsc5.html) для построения звездного неба и исходных(данных) характеристик звезд
Также приложение содержит данные о созвездиях и известных именах звезд. Более подробно с используемыми данными можно ознакомиться [здесь.](https://github.com/SharafeevRavil/astrolib.Unity/tree/master/AstrolibUI/Assets/Resources)

## Скриншоты приложения
Просмотр звездного неба
![image](https://github.com/SharafeevRavil/astrolib.Unity/assets/30022504/c785e308-fc4e-4311-ab25-d66046d5945d)

Просмотр созвездий с сеткой right ascension и declination
![image](https://github.com/SharafeevRavil/astrolib.Unity/assets/30022504/d654b309-f6e9-4f77-b440-3c5b18a38f6c)

Просмотр характеристик звезд и сравнение яркостей
![image](https://github.com/SharafeevRavil/astrolib.Unity/assets/30022504/4a611fd9-8e85-4382-82b9-31c91a727481)
