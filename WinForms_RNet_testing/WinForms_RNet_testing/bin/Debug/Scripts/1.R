library(xlsx) #подключение библиотеки

setwd('E:/Data') #установка рабочей директории
mydata <- read.xlsx('123.xlsx', 1) #чтение xlsx файла
d <- dist(mydata) # euclidean distances between the rows
fit <- cmdscale(d,eig=TRUE, k=2) # k is the number of dim

write.xlsx(fit$points, 'E:/data/mydata.xlsx') # создание Excel файла