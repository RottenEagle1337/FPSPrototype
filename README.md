# FPSPrototype
	Ссылка на билд - https://disk.yandex.ru/d/gh9X6Ix5G6AbQQ
 
# Использованные в проекте ассеты:
	1) https://assetstore.unity.com/packages/3d/props/polygon-starter-pack-low-poly-3d-art-by-synty-156819
	2) https://assetstore.unity.com/packages/3d/props/guns/stylized-m4-assault-rifle-with-scope-complete-kit-with-gunshot-v-178197
	3) https://assetstore.unity.com/packages/3d/props/exterior/low-poly-fence-pack-61661
	4) https://assetstore.unity.com/packages/vfx/particles/simple-fx-cartoon-particles-67834

# Управление:
	W/A/S/D - передвижение
	Space - прыжок
	R - перезарядка
	E - пополнить патроны ( работает только рядом с AmmoStation )
	LMB - стрельба
	RMB - прицеливание

# Общая информация о проекте:
	1) Есть мишение, у которых определённый запас хп. По истечению хп мишень уходит на перезарядку( складывается )
	2) Есть подвижные коробки, которые реагируют на выстрелы
	3) Можно пополнить запас аммуниции рядом с шерифом ( AmmoStation )
	4) Реализована пауза и настройка чувствительности
	5) Для оптимизации добавил Oclussion Culling и запёк освещение для статичных обьектов
	6) Использован old input system, не реализовано сохранение
	7) Добавлены пост эффекты( global volume )
	8) Добавлены звуки
	9) Класс оружия не стал дробить, но там можно выделить:
		a) механнику выстрела
		b) вычисление и применение отдачи
		c) менеджмент аммуниции
	10) Добавлена реакция оружия на перемещение

# Баги:
	1) Оружие может застрять в текстурах в билде, хотя в edit mode всё нормально. Render pipeline настроил корректно
