library(xlsx) #����������� ����������

setwd('E:/Data') #��������� ������� ����������
mydata <- read.xlsx('123.xlsx', 1) #������ xlsx �����
d <- dist(mydata) # euclidean distances between the rows
fit <- cmdscale(d,eig=TRUE, k=2) # k is the number of dim

write.xlsx(fit$points, 'E:/data/mydata.xlsx') # �������� Excel �����