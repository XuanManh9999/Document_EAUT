#include<bits/stdc++.h>
#include <Windows.h>
using namespace std;
void SET_COLOR(int color);
struct monhoc{
	double toan, ly, tienganh,ctglgt, tb;
	string tenmon, sotin;
	monhoc(){
	}
	monhoc(string ten_m, string so_t, double toa_n, double l_y, double english, double ctdlgt) {
		tenmon = ten_m;
		sotin = so_t;
		toan = toa_n;
		ly = l_y;
		tienganh = english;
		ctglgt = ctdlgt;
		tb = (toan + ly + tienganh + ctglgt) / 4;
	}
};
struct diem{
	string msv;
	string mamonhoc;
	string ten;
	diem(){
		
	}
	diem(string MSV, string tensv, string MMH){
		msv = MSV;
		ten = tensv;
		mamonhoc = MMH;
	}
};
struct node {
	monhoc data1;
	diem data2;
	node * next;// tro den thang tiep theo
	node * preve;// tro den thang dang truoc no
};
node * makeNode(monhoc x, diem y) {
	node *newNode = new node;// tao ra 1 node moi
	newNode->data1 = x;
	newNode->data2 = y;
	newNode->next = NULL;
	newNode->preve = NULL;
	return newNode;// tao ra 1 node thanh cong
}
void duyet(node *head) {
	while (head != NULL) {
		SET_COLOR(3);
		cout << "----------------------------------------------------------" << endl;
		cout << "MSV: " << head->data2.msv<< endl; 	
		cout << "TEN_SV: " << head->data2.ten << endl;
		cout << "MA_MON_HOC: " << head->data2.mamonhoc << endl;
		cout << "TEN_BO_MON: " << head->data1.tenmon << endl;
		cout << "SO_TIN: " << head->data1.sotin << endl;
		cout << "DIEM_TOAN: " << fixed << setprecision(2)  << head->data1.toan << endl;
		cout << "DIEM_LY: " << fixed << setprecision(2)  << head->data1.ly << endl;
		cout << "DIEM_TIENGANH: " << fixed << setprecision(2)  << head->data1.tienganh <<endl;
		cout << "DIEM_CTDLGT: " << fixed << setprecision(2)  << head->data1.ctglgt << endl;
		cout << "DIEM_TB: " << fixed << setprecision(2) << head->data1.tb << endl;
		cout << "----------------------------------------------------------" << endl;
		head = head->next;
	}
	cout << endl;
}
void duyet_nguoc(node *head) {
	while (head->next != NULL) {
		head = head->next;
	}
	while (head != NULL) {
		SET_COLOR(3);
		cout << "----------------------------------------------------------" << endl;
		cout << "MSV: " << head->data2.msv<< endl; 	
		cout << "TENSV: " << head->data2.ten << endl;
		cout << "MAMONHOC: " << head->data2.mamonhoc << endl;
		cout << "TEN_BO_MON: " << head->data1.tenmon << endl;
		cout << "SO_TIN: " << head->data1.sotin << endl;
		cout << "DIEM_TOAN: " << fixed << setprecision(2)  << head->data1.toan << endl;
		cout << "DIEM_LY: " << fixed << setprecision(2)  << head->data1.ly << endl;
		cout << "DIEM_TIENGANH: " << fixed << setprecision(2)  << head->data1.tienganh <<endl;
		cout << "DIEM_CTDLGT: " << fixed << setprecision(2)  << head->data1.ctglgt << endl;
		cout << "DIEM_TB: " << fixed << setprecision(2) << head->data1.tb << endl;
		cout << "----------------------------------------------------------" << endl;
		head = head->preve;
		}
	cout << endl;
}
void selection_sort(node **head) {
	for (node *i = *head; i != NULL; i = i->next) {
		node *min = i;// cho no vi tri thang gia si la min
		for (node *j = i->next; j != NULL; j = j->next) {
			if (min->data1.toan > j->data1.toan) {
				min = j;// cho no bang min
			}
		}
		swap(i->data2.ten, min->data2.ten);
		swap(i->data1.tb, min->data1.tb);
		swap(i->data1.toan, min->data1.toan);
		swap(i->data1.ly, min->data1.ly);
		swap(i->data1.tienganh, min->data1.tienganh);
		swap(i->data1.ctglgt, min->data1.ctglgt);
		swap(i->data1.tenmon, min->data1.tenmon);
		swap(i->data1.sotin, min->data1.sotin);
		swap(i->data2.msv, min->data2.msv);
		swap(i->data2.mamonhoc, min->data2.mamonhoc);
	}
}
void themcuoi (node **head,monhoc x,diem y) {
	node *newNode = makeNode(x, y);
	if (*head == NULL) {
		*head = newNode;
		return;
	}else {
		node *tmp = *head;
		while (tmp->next != NULL) {
			tmp = tmp->next;
		}
		tmp->next = newNode;
		newNode->preve = tmp;
	}	
}
void tb_selection_sort(node **head) {
	for (node *i = *head; i != NULL; i = i->next) {
		node *min = i;// cho no vi tri thang gia si la min
		for (node *j = i->next; j != NULL; j = j->next) {
			if (min->data1.tb > j->data1.tb ){
				min = j;// cho no bang min
			}
		}
		// hoan doi toan bo
		swap(i->data2.ten, min->data2.ten);
		swap(i->data1.tb, min->data1.tb);
		swap(i->data1.toan, min->data1.toan);
		swap(i->data1.ly, min->data1.ly);
		swap(i->data1.tienganh, min->data1.tienganh);
		swap(i->data1.ctglgt, min->data1.ctglgt);
		swap(i->data1.tenmon, min->data1.tenmon);
		swap(i->data1.sotin, min->data1.sotin);
		swap(i->data2.msv, min->data2.msv);
		swap(i->data2.mamonhoc, min->data2.mamonhoc);
	}
}
double cao(node *head) {
	double max_ = INT_MIN;
	while (head != NULL) {
		if (head->data1.tb > max_) {
			max_ =head->data1.tb;
		}
		head = head->next;
	}
	return max_;
}
double thap(node *head) {
	double min_ = INT_MAX;
	while (head != NULL) {
		if (head->data1.tb < min_) {
			min_ =head->data1.tb;
		}
		head = head->next;
	}
	return min_;
}
void inthukhoa(node *head) {
	double diem = cao(head);
	while (head != NULL) {
		if (head->data1.tb == diem) {
			SET_COLOR(3);
			cout << "----------------------------------------------------------" << endl;
			cout << "BAN SINH VIEN CO DIEM TB CAO NHAT LA: " << head->data2.ten << endl;
			cout << "DIEM_TB: " << fixed << setprecision(2) << head->data1.tb << endl;
		}
		head = head->next;
	}
	cout << endl;
}
void inthap(node *head) {
	double diem = thap(head);
	while (head != NULL) {
		if (head->data1.tb == diem) {
			SET_COLOR(3);
			cout << "BAN SINH VIEN CO DIEM TB THAP NHAT LA: " << head->data2.ten << endl;
			cout << "DIEM_TB: " << fixed << setprecision(2) << head->data1.tb << endl;
			cout << "----------------------------------------------------------" << endl;
		}
		head = head->next;
	}
	cout << endl;
}
void SET_COLOR(int color)
{
	WORD wColor;
     

     HANDLE hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
     CONSOLE_SCREEN_BUFFER_INFO csbi;
     if(GetConsoleScreenBufferInfo(hStdOut, &csbi))
     {
          wColor = (csbi.wAttributes & 0xF0) + (color & 0x0F);
          SetConsoleTextAttribute(hStdOut, wColor);
     }
}
int main() {
	node  * head =NULL;
	while (1) {
		SET_COLOR(2);
		cout<< "------------------------/3-------------------------" << endl;
		cout << "1. NHAP VAO SINH VIEN: " << endl;
		cout << "2. SX SV THEO DIEM TOAN: " << endl;
		cout << "3. SX SV THEO CHIEU TANG DAN DIEM TRUNG BINH: " << endl;
		cout << "4. TIM SINH VIEN CO DIEM TRUNG BINH MAX AND MIN: " << endl;
		cout << "5. DUYET SINH VIEN: " << endl;
		cout << "6. DUYET SINH VIEN THEO CHIEU NGUOC LAI: " << endl;
		cout << "$. EXIT: " << endl;
		cout<< "------------------------/3-------------------------" << endl;
		char lc;
		SET_COLOR(6);
		cout << "NHAP VAO LUA CHON CUA BAN: ";
		cin >> lc;
		cin.ignore();
		if(lc == '1') {
			SET_COLOR(7);
			string msv,tensv, mamon, tenmon, sotin;
			cout << "NHAP_VAO_MSV: ";
			getline(cin, msv);
			cout << "NHAP_VAO_TEN_SV: ";
			getline(cin, tensv);
			cout << "NHAP_VAO_MA_BO_MON: ";
			getline(cin, mamon);
			cout << "NHAP_VAO_TEN_BO_MON: ";
			getline(cin, tenmon);
			cout << "NHAP_VAO_SO_TIN: ";
			getline(cin, sotin);
			double toan, ly, tienganh, ctdlgt;
			cout << "NHAP_VAO_DIEM_TOAN: ";
			cin >> toan;
			cout << "NHAP_VAO_DIEM_LY: ";
			cin >> ly;
			cout << "NHAP_VAO_DIEM_TIENG_ANH: ";
			cin >> tienganh;
			cout << "NHAP_VAO_DIEM_CTDLGT: ";
			cin >> ctdlgt;
			themcuoi(&head, monhoc(tenmon, sotin, toan, ly, tienganh, ctdlgt), diem(msv, tensv, mamon));  
		}else if (lc == '2') {
			selection_sort(&head);
		}else if (lc == '3') {
			tb_selection_sort(&head);
		}else if (lc == '4') {
			inthukhoa(head);
			inthap(head);
		}else if(lc == '5') {
			duyet(head);
		}else if (lc == '6') {
			duyet_nguoc(head);
		}else if(lc == '$') {
			break;
		}
	}
	return 0;
}
