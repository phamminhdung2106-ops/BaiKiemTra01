# I. PHẦN LÝ THUYẾT & CÂU HỎI NGẮN

## Câu 1: Phân biệt Value Types và Reference Types trong C#

### Value Types (Kiểu giá trị)

- Lưu trực tiếp giá trị của biến trong vùng nhớ.
- Thường được lưu trên Stack.
- Khi gán một biến cho biến khác thì giá trị được sao chép sang biến mới.
- Ví dụ: `int`, `float`, `double`, `bool`, `struct`.

### Reference Types (Kiểu tham chiếu)

- Biến lưu địa chỉ tham chiếu đến đối tượng được lưu trên Heap.
- Khi gán một biến cho biến khác thì hai biến cùng tham chiếu đến một đối tượng.
- Ví dụ: `class`, `object`, `array`, `string`.

### Ví dụ Value Type

```csharp
int a = 10;
int b = a;
b = 20;

Sau khi thực hiện, a vẫn bằng 10 vì b chỉ nhận bản sao giá trị của a.

Ví dụ Reference Type
class SinhVien
{
    public string ten;
}

SinhVien sv1 = new SinhVien();
SinhVien sv2 = sv1;

sv1 và sv2 cùng tham chiếu đến một đối tượng trên Heap.

Tóm lại
Value Types	Reference Types
Lưu trực tiếp giá trị	Lưu địa chỉ tham chiếu
Thường liên quan đến Stack	Đối tượng được lưu trên Heap
Gán biến → sao chép giá trị	Gán biến → sao chép tham chiếu
Ví dụ: int, float, struct	Ví dụ: class, array, object
Câu 2: Init-only Properties (init) khác gì set thông thường?
set

set cho phép thay đổi giá trị của thuộc tính bất cứ lúc nào sau khi đối tượng được tạo.

Ví dụ:

class SinhVien
{
    public string MaSV { get; set; }
}

SinhVien sv = new SinhVien();
sv.MaSV = "SV01";
sv.MaSV = "SV02";

Giá trị MaSV có thể thay đổi sau khi tạo đối tượng.

init

init chỉ cho phép gán giá trị khi khởi tạo đối tượng. Sau khi đối tượng được tạo thì không thể thay đổi giá trị.

Ví dụ:

class SinhVien
{
    public string MaSV { get; init; }
}

Có thể khởi tạo:

SinhVien sv = new SinhVien
{
    MaSV = "SV01"
};

Nhưng sau đó:

sv.MaSV = "SV02";

Sẽ báo lỗi.

Trường hợp sử dụng

init phù hợp với những thông tin không muốn thay đổi sau khi tạo đối tượng, ví dụ:

Mã sinh viên
Mã nhân viên
Mã sản phẩm
Tóm lại
set: Có thể thay đổi giá trị sau khi tạo đối tượng.
init: Chỉ được gán giá trị khi khởi tạo đối tượng.
Câu 3: Phân biệt virtual và override trong tính đa hình
virtual

virtual được khai báo trong lớp cha.

Nó cho phép lớp con ghi đè lại phương thức đó.

Ví dụ:

class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Dong vat keu");
    }
}
override

override được khai báo trong lớp con.

Nó dùng để ghi đè phương thức virtual của lớp cha.

Ví dụ:

class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Cho sua");
    }
}

Khi sử dụng:

Animal a = new Dog();
a.Sound();

Kết quả:

Cho sua
Tóm lại
virtual: Khai báo ở lớp cha, cho phép lớp con ghi đè.
override: Khai báo ở lớp con, thực hiện việc ghi đè phương thức của lớp cha.

Đây là cơ chế giúp C# thực hiện tính đa hình (Polymorphism).

Câu 4: Tại sao thành phần static không thể truy xuất thông qua Object Instance?

Thành phần được khai báo static thuộc về Class, không thuộc về từng Object Instance.

Ví dụ:

class SinhVien
{
    public static int soLuong = 0;
}

Ta truy xuất thành phần static thông qua tên lớp:

SinhVien.soLuong++;

Không cần tạo đối tượng.

Ví dụ:

SinhVien sv = new SinhVien();

Đối tượng sv không có một bản sao riêng của biến soLuong.

Vì soLuong là static nên tất cả các đối tượng của lớp SinhVien đều dùng chung một biến soLuong.

Tóm lại
Thành phần static thuộc về Class.
Thành phần thông thường thuộc về Object Instance.
static nên được truy xuất thông qua tên lớp.

Ví dụ:

SinhVien.soLuong++;

Thay vì truy xuất thông qua:

sv.soLuong++;

Kết luận: static được dùng khi muốn một thành phần được chia sẻ chung cho tất cả các đối tượng của một lớp.
