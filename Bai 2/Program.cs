using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AutoSpeed;

// ============ A. Abstract class PhuongTien ============
public abstract class PhuongTien
{
    private string _maPT = "PT000";
    private string _tenHang = string.Empty;
    private int _namSanXuat;
    private decimal _giaGoc;

    protected PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
    {
        MaPT = maPT;
        TenHang = tenHang;
        NamSanXuat = namSanXuat;
        GiaGoc = giaGoc;
    }

    // Trống / chỉ chứa khoảng trắng -> dùng mặc định "PT000"
    public string MaPT
    {
        get => _maPT;
        set => _maPT = string.IsNullOrWhiteSpace(value) ? "PT000" : value.Trim();
    }

    public string TenHang
    {
        get => _tenHang;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên hãng không được để trống!");
            _tenHang = value.Trim();
        }
    }

    public int NamSanXuat
    {
        get => _namSanXuat;
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Năm sản xuất không hợp lệ!");
            _namSanXuat = value;
        }
    }

    public decimal GiaGoc
    {
        get => _giaGoc;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Giá gốc phải lớn hơn 0!");
            _giaGoc = value;
        }
    }

    public abstract decimal TinhGiaLanBanh();

    public virtual string GetInfo() =>
        $"Mã: {MaPT} | Hãng: {TenHang} | Năm SX: {NamSanXuat} | Giá gốc: {GiaGoc:N0} VNĐ";
}

// ============ B. Class OTo ============
public class OTo : PhuongTien
{
    private int _soChoNgoi;
    private double _dungTichDongCo;

    public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc,
               int soChoNgoi, double dungTichDongCo)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        SoChoNgoi = soChoNgoi;
        DungTichDongCo = dungTichDongCo;
    }

    public int SoChoNgoi
    {
        get => _soChoNgoi;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
            _soChoNgoi = value;
        }
    }

    public double DungTichDongCo
    {
        get => _dungTichDongCo;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
            _dungTichDongCo = value;
        }
    }

    public override decimal TinhGiaLanBanh()
    {
        decimal truocBa = GiaGoc * 0.12m;

        // <= 9 chỗ: thêm thuế TTĐB 30%; > 9 chỗ: trước bạ 10%
        return SoChoNgoi <= 9
            ? GiaGoc + truocBa + GiaGoc * 0.30m
            : GiaGoc + GiaGoc * 0.10m;
    }

    public override string GetInfo() =>
        $"[Ô tô] {base.GetInfo()} | Số chỗ: {SoChoNgoi} | Dung tích động cơ: {DungTichDongCo} L";
}

// ============ C. Class XeMay ============
public class XeMay : PhuongTien
{
    private int _dungTichXylanh;

    public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        DungTichXylanh = dungTichXylanh;
    }

    public int DungTichXylanh
    {
        get => _dungTichXylanh;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Dung tích xylanh phải lớn hơn 0!");
            _dungTichXylanh = value;
        }
    }

    public override decimal TinhGiaLanBanh() =>
        DungTichXylanh < 175
            ? GiaGoc + GiaGoc * 0.02m
            : GiaGoc + GiaGoc * 0.05m;

    public override string GetInfo() =>
        $"[Xe máy] {base.GetInfo()} | Dung tích xylanh: {DungTichXylanh} cc";
}

// ============ D. Class QuanLyPhuongTien ============
public class QuanLyPhuongTien
{
    private readonly List<PhuongTien> _ds = new();

    public void AddPhuongTien(PhuongTien pt)
    {
        ArgumentNullException.ThrowIfNull(pt);
        _ds.Add(pt);
    }

    public void DisplayAll()
    {
        if (_ds.Count == 0)
        {
            Console.WriteLine("Danh sách rỗng.");
            return;
        }

        foreach (var pt in _ds)
        {
            Console.WriteLine(pt.GetInfo());
            Console.WriteLine($"   => Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ"); // đa hình
        }
    }

    public PhuongTien? FindMaxGiaLanBanh() =>
        _ds.Count == 0 ? null : _ds.MaxBy(pt => pt.TinhGiaLanBanh());

    public List<PhuongTien> SearchByName(string keyword) =>
        _ds.Where(pt => pt.TenHang.Contains(keyword ?? string.Empty,
                                            StringComparison.OrdinalIgnoreCase))
           .ToList();
}

// ============ Kiểm thử ============
public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var vn = CultureInfo.GetCultureInfo("vi-VN");
        CultureInfo.DefaultThreadCurrentCulture = vn;
        CultureInfo.CurrentCulture = vn;

        // TC01: năm sản xuất không hợp lệ
        try
        {
            _ = new OTo("OT00", "Toyota", 1850, 1_000_000_000m, 5, 2.0);
            Console.WriteLine("TC01: FAIL (không ném ngoại lệ)");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"TC01: PASS - {ex.Message}");
        }

        // TC02
        var oto = new OTo("OT01", "Toyota", 2023, 1_000_000_000m, 5, 2.0);
        Console.WriteLine($"TC02: {(oto.TinhGiaLanBanh() == 1_420_000_000m ? "PASS" : "FAIL")} - {oto.TinhGiaLanBanh():N0}");

        // TC03
        var xe = new XeMay("XM01", "Honda", 2024, 50_000_000m, 150);
        Console.WriteLine($"TC03: {(xe.TinhGiaLanBanh() == 51_000_000m ? "PASS" : "FAIL")} - {xe.TinhGiaLanBanh():N0}");

        // TC04: đa hình
        var ql = new QuanLyPhuongTien();
        ql.AddPhuongTien(oto);
        ql.AddPhuongTien(xe);
        Console.WriteLine("TC04:");
        ql.DisplayAll();

        // TC05
        var max = ql.FindMaxGiaLanBanh();
        Console.WriteLine($"TC05: {(max == oto ? "PASS" : "FAIL")} - {max?.GetInfo()} => {max?.TinhGiaLanBanh():N0}");

        // SearchByName
        Console.WriteLine("Tìm 'hon':");
        foreach (var pt in ql.SearchByName("hon"))
            Console.WriteLine("  " + pt.GetInfo());
    }
}