âu 1: Sự khác nhau giữa Value Types và Reference Types trong C#
Value Types (Kiểu giá trị): Lưu trực tiếp giá trị của biến, thường được lưu trên Stack. Khi gán một biến cho biến khác thì giá trị được sao chép sang biến mới.
Ví dụ: int, float, double, bool, struct.
Reference Types (Kiểu tham chiếu): Biến lưu địa chỉ tham chiếu đến đối tượng được lưu trên Heap. Khi gán một biến cho biến khác thì hai biến cùng tham chiếu đến một đối tượng trên Heap.
Ví dụ: class, object, string, array.

Ví dụ:

int a = 10;
int b = a;
b = 20;

→ a vẫn bằng 10 vì b chỉ nhận bản sao giá trị của a.

class SinhVien
{
    public string ten;
}

SinhVien sv1 = new SinhVien();
SinhVien sv2 = sv1;

→ sv1 và sv2 cùng tham chiếu đến một đối tượng trên Heap.

Tóm lại:

Value Types	Reference Types
Lưu trực tiếp giá trị	Lưu địa chỉ tham chiếu
Thường ở Stack	Đối tượng ở Heap
Gán → sao chép giá trị	Gán → sao chép tham chiếu
int, float, struct...	class, array, object...
làm hết 4 câu
I. PHẦN LÝ THUYẾT & CÂU HỎI NGẮN
Câu 1: Phân biệt Value Types và Reference Types trong C#
Value Types (Kiểu giá trị): Lưu trực tiếp giá trị của biến trong vùng nhớ. Thường được lưu trên Stack. Khi gán biến này cho biến khác thì giá trị được sao chép.
Ví dụ: int, float, double, bool, struct.
Reference Types (Kiểu tham chiếu): Biến lưu địa chỉ tham chiếu đến đối tượng được lưu trên Heap. Khi gán biến này cho biến khác thì hai biến cùng tham chiếu đến một đối tượng.
Ví dụ: class, object, array, string.
Ví dụ:
int a = 10;
int b = a;
b = 20;
→ a vẫn bằng 10.
class SinhVien
{
    public string ten;
}
SinhVien sv1 = new SinhVien();
SinhVien sv2 = sv1;
→ sv1 và sv2 cùng tham chiếu đến một đối tượng trên Heap.
Tóm lại: Value Type lưu giá trị, còn Reference Type lưu tham chiếu đến đối tượng.
Câu 2: Init-only Properties (init) khác gì set thông thường?
set: Cho phép thay đổi giá trị thuộc tính bất cứ lúc nào sau khi đối tượng được tạo.
init: Chỉ cho phép gán giá trị khi khởi tạo đối tượng, sau đó không thể thay đổi.
Ví dụ:
class SinhVien
{
    public string MaSV { get; init; }
}
Có thể:
SinhVien sv = new SinhVien { MaSV = "SV01" };
Nhưng sau đó không thể:
sv.MaSV = "SV02"; // Lỗi
Trường hợp sử dụng: Dùng init khi muốn thông tin chỉ được thiết lập lúc tạo đối tượng và không muốn bị thay đổi sau đó, ví dụ Mã sinh viên, Mã nhân viên, Mã sản phẩm.
Câu 3: Phân biệt virtual và override trong tính đa hình
virtual: Được khai báo ở lớp cha, cho phép lớp con thay đổi cách thực hiện phương thức đó.
override: Được khai báo ở lớp con, dùng để ghi đè phương thức virtual của lớp cha.
Ví dụ:
class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Dong vat keu");
    }
}
class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Cho sua");
    }
}
Khi gọi:
Animal a = new Dog();
a.Sound();
→ Kết quả: "Cho sua".
Tóm lại: virtual cho phép ghi đè, còn override thực hiện việc ghi đè ở lớp con.
Câu 4: Tại sao thành phần static không thể truy xuất thông qua Object Instance?
static thuộc về lớp (Class) chứ không thuộc về từng đối tượng được tạo bằng new.
Ví dụ:
class SinhVien
{
    public static int soLuong = 0;
}
Ta truy xuất bằng tên lớp:
SinhVien.soLuong++;
Không cần tạo đối tượng:
SinhVien sv = new SinhVien();
Nếu truy xuất:
sv.soLuong;
→ Không đúng cách vì soLuong thuộc về Class, không thuộc riêng đối tượng sv.
