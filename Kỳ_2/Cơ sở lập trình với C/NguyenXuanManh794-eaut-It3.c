#include <stdio.h>
#include <stdlib.h>
#include <string.h>
//em khai bao ham len nay, de thay nhin thay ham main luon a
void printfMenu();
void family();
void tinhToan();
void Sv();
int main(int argc, char *argv[]) {
	int n;
	
	do{
		printfMenu();
		scanf("%d", &n);
		switch(n)
		{
			case 1:
				family();
				break;
			case 2:
				tinhToan();
				break;
			case 3:
				Sv();
				break;		
		}
	} while (n != 0) ;
	return 0;
}
void printfMenu() 
{
	printf("\n*======================================================*\n");
	printf("\n*vui long nhap vao su lua tron cua ban              *\n");
	printf("\n*Nhap Vao 1 --> Thong Tin Gia Dinh                  *\n");
	printf("\n*Nhap Vao 2 --> So Chia Het Cho 5                   *\n");
	printf("\n*Nhap Vao 3 --> Thong Tin Sinh Vien THi Lap Trinh   *\n");
	printf("\n*Nhap Vao 0 --> De Thoat\n 						  *\n");
}
void family()
{
	int yobFather;
	int yobMother;
	char nameMother[20];
	char nameFather[20];
	printf("nhap vao ten cua bo\n");
	scanf("%s", &nameFather);
	printf("\nnhap vao Tuoi cua bo\n");
	scanf("%d", &yobFather);
	printf("\nnhap vao ten cua me\n");
	scanf("%s", nameMother);
	printf("\nnhap vao tuoi cua me\n");
	scanf("%d", &yobMother);
	printf("Ten Cua Bo La = %s -- Tuoi = %d--|Ten Cua Me La = %s -- Tuoi Cua Me La = %d\n", nameFather, yobFather, nameMother, yobMother);	
}
void tinhToan() {
	int n;
	int tong = 0;
	printf("nhap vao n\n");
	scanf("%d", &n);
	for (int i = 1; i < n; i++)//em cho no nho hon n, vi yeu cau de bai la cac so nho hon n chia het cho 5 
		if (i % 5 == 0){//vi ban chat i++ tang tu tu nen e dung if de kiem tra
			printf("\n cac so nho hon n chia het cho 5 la\n|%d ", i);
				tong += i; //thuat toan nhoi con heo dat
		}
	printf("\ntong cac so nho hon n chia het cho 5 = %d", tong);
}
void Sv()
{
	int c[10000]; 
	int count = 0;
	int n;
	printf("Xin Moi Ban Nhap Vao So sinh vien\n");
	scanf("%d", &n);
	printf("Xin moi Ban Nhap vao sinh vien | diem thi\n");
	for (int i = 0; i < n; i++)
	{
		printf("c[%d]= ", i);
		scanf("%d", &c[i]);
		count++;//day la bien dung de dem so luong sinh vien
	}
	printf("\n");
	printf("Diem Cua Tung Sinh Vien La\n");
	for (int i = 0; i < n; i++)
		printf("c[%d]= %d ", i, c[i]);
	printf("\n");
	printf("so luong sinh vien la = %d", count);
	//dem so luong sinh vien co diem trung binh >= 5;
	printf("\n");
	int dem = 0;
	for (int i = 0 ; i < n; i++)
		if (c[i] >= 5)
			dem++;//day la bien dung de dem so luong sinh vien diem: >= 5
	printf("so luong sinh vien co diem trung binh >= 5 la %d\n", dem);
	printf("\n");
	int max = c[0];
	int t;
	for (int i = 0; i < n; i++)//doan nay e muon sap xep thoi thay a
	{
		if(max < c[i]);
			t = max; 
			max = c[i];
			c[i] = t;
	}
	for(int i =0; i < n; i++)
		printf("c[%d] =%d ", i, c[i]);
	printf("\n");
	//quy dinh diem cao la tu 7;
	int diemCao = 7;
	int viTri;
	for (int i = 0; i < n; i++)
	{
		if(c[i] >= diemCao)
		{
			viTri = i;
			printf("vi tri diem cao\n = c[%d]\n", viTri);
		}
	}
}
