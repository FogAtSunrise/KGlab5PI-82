using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGlab5PI_82
{
    class RedactorComand
    {
        int X1, X2, Y1, Y2;
        int sR, sG, sB;
        private int WINSIZE;
        public int _width;
        public int _height;
        private string nameFile;
        private Bitmap rawImage; 
        private Dictionary<int, int> tableBrightnessForGistogramm;
        readonly int COUNCOLOR = 256;
        readonly int BLACCOLOR = 255;
        readonly int WHITECOLOR = 0;
        private Bitmap STARTimage { get; set; } //исходное изображение
        private Bitmap FIRSTimage { get; set; } //исходное изображение
        public Bitmap CURRENTimage { get; set; } // преобразованное




        public Bitmap AddSpeckleNoise()
        {
            float v = 0.04f;
            var res = (Bitmap)CURRENTimage.Clone();
            var rnd = new Random();
            var stdDev = Math.Sqrt(v);//девиация - корень из дисперсии

            using (var wr = new ImageWrapper(res))
                foreach (var p in wr)
                {
                    var c = wr[p];
                    var noise = (rnd.NextDouble() - 0.5f) * 2 * stdDev;//равномерное распр со средним = 0, дисперсия = v
                    wr.SetPixel(p, c.R + noise * c.R, c.G + noise * c.G, c.B + noise * c.B);//Id=Is+n*Is
                }

            return CURRENTimage = res;
        }
    


    /// <summary>
    /// Обертка над Bitmap для быстрого чтения и изменения пикселов.
    /// Также, класс контролирует выход за пределы изображения: при чтении за границей изображения - возвращает DefaultColor, при записи за границей изображения - игнорирует присвоение.
    /// </summary>
    public class ImageWrapper : IDisposable, IEnumerable<Point>
        {
            /// <summary>
            /// Ширина изображения
            /// </summary>
            public int Width { get; private set; }
            /// <summary>
            /// Высота изображения
            /// </summary>
            public int Height { get; private set; }
            /// <summary>
            /// Цвет по-умолачнию (используется при выходе координат за пределы изображения)
            /// </summary>
            public Color DefaultColor { get; set; }

            private byte[] data;//буфер исходного изображения
            private byte[] outData;//выходной буфер
            private int stride;
            private BitmapData bmpData;
            private Bitmap bmp;

            /// <summary>
            /// Создание обертки поверх bitmap.
            /// </summary>
            /// <param name="copySourceToOutput">Копирует исходное изображение в выходной буфер</param>
            public ImageWrapper(Bitmap bmp, bool copySourceToOutput = false)
            {
                Width = bmp.Width;
                Height = bmp.Height;
                this.bmp = bmp;

                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                stride = bmpData.Stride;

                data = new byte[stride * Height];
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, data, 0, data.Length);

                outData = copySourceToOutput ? (byte[])data.Clone() : new byte[stride * Height];
            }

            /// <summary>
            /// Возвращает пиксел из исходнго изображения.
            /// Либо заносит пиксел в выходной буфер.
            /// </summary>
            public Color this[int x, int y]
            {
                get
                {
                    var i = GetIndex(x, y);
                    return i < 0 ? DefaultColor : Color.FromArgb(data[i + 3], data[i + 2], data[i + 1], data[i]);
                }

                set
                {
                    var i = GetIndex(x, y);
                    if (i >= 0)
                    {
                        outData[i] = value.B;
                        outData[i + 1] = value.G;
                        outData[i + 2] = value.R;
                        outData[i + 3] = value.A;
                    };
                }
            }

            /// <summary>
            /// Возвращает пиксел из исходнго изображения.
            /// Либо заносит пиксел в выходной буфер.
            /// </summary>
            public Color this[Point p]
            {
                get { return this[p.X, p.Y]; }
                set { this[p.X, p.Y] = value; }
            }

            /// <summary>
            /// Заносит в выходной буфер значение цвета, заданные в double.
            /// Допускает выход double за пределы 0-255.
            /// </summary>
            public void SetPixel(Point p, double r, double g, double b)
            {
                if (r < 0) r = 0;
                if (r >= 256) r = 255;
                if (g < 0) g = 0;
                if (g >= 256) g = 255;
                if (b < 0) b = 0;
                if (b >= 256) b = 255;

                this[p.X, p.Y] = Color.FromArgb((int)r, (int)g, (int)b);
            }

            int GetIndex(int x, int y)
            {
                return (x < 0 || x >= Width || y < 0 || y >= Height) ? -1 : x * 4 + y * stride;
            }

            /// <summary>
            /// Заносит в bitmap выходной буфер и снимает лок.
            /// Этот метод обязателен к исполнению (либо явно, лмбо через using)
            /// </summary>
            public void Dispose()
            {
                System.Runtime.InteropServices.Marshal.Copy(outData, 0, bmpData.Scan0, outData.Length);
                bmp.UnlockBits(bmpData);
            }

            /// <summary>
            /// Перечисление всех точек изображения
            /// </summary>
            public IEnumerator<Point> GetEnumerator()
            {
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        yield return new Point(x, y);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Меняет местами входной и выходной буферы
            /// </summary>
            public void SwapBuffers()
            {
                var temp = data;
                data = outData;
                outData = temp;
            }
        }


        public RedactorComand(int size)
        {
            WINSIZE = size;

           
        }

        public bool loadImage()
        {
            OpenFileDialog winDialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            winDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (winDialog.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    nameFile = winDialog.FileName;
                    rawImage = new Bitmap(nameFile);
                    _width = rawImage.Width;
                    _height = rawImage.Height;

                    
                    if (rawImage.Width > WINSIZE || rawImage.Height > WINSIZE)
                    {  
                        if (rawImage.Width >= rawImage.Height)
                        { // если она шире
                            _width = WINSIZE;
                            _height = (int)(rawImage.Height * ((float)WINSIZE / rawImage.Width));
                        }
                        else
                        {
                            _height = WINSIZE;
                            _width = (int)(rawImage.Width * ((float)WINSIZE / rawImage.Height));
                        }
                    }
                    X1 = 0;
                    X2 = _width;
                    Y1 = 0;
                    Y2 = _height;
                    CURRENTimage = new Bitmap(rawImage, new Size(_width, _height));
                    applyChanges();
                    applyStartChanges();
                    CalculateAverageRgb();
                    return true;
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Не удалось открыть изображение",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }
            }
            return false;
        }

        //получить текущее изображение
        public Bitmap getImageForPrint()
        {
            return CURRENTimage;
        }

        //получить яркость пикселя в текущем состоянии изображения
        public int getBrightPixel(int width, int height)
        {
            //  Color color = FIRSTimage.GetPixel(width, height);
            Color color = CURRENTimage.GetPixel(width, height);

            return (int)(0.299 * color.R + 0.5876 * color.G + 0.114 * color.B);
        }



        public Dictionary<int, int> getBrigGistogramm()
        {
            
            tableBrightnessForGistogramm = new Dictionary<int, int>();
            for (int i = 0; i < COUNCOLOR; i++)
            {
                tableBrightnessForGistogramm.Add(i, 0);
            }

            for (int i = X1; i < X2; i++)
            {
                for (int j = Y1; j < Y2; j++)
                {
                    tableBrightnessForGistogramm[getBrightPixel(i, j)]++;
                }
            }
            return tableBrightnessForGistogramm;
        }

// Изменение яркости на величину value
        public void changeBrightness(int value)
        {
            
            Bitmap helpMap = new Bitmap(_width, _height); 


            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Color source = FIRSTimage.GetPixel(i, j);
                    Color result;
                    if (i >= X1 && i < X2 && j >= Y1 && j < Y2)
                    {
                        
                       result = createColor(source.R + value, source.G + value, source.B + value);
                    }
                    else
                        result = createColor(source.R, source.G, source.B);
                    helpMap.SetPixel(i, j, result);
                }
            }

            CURRENTimage = helpMap;
        }

        public Bitmap Svertka(Bitmap foto, int height, int width, double[,] kernel) // принимает параметры ядра
        {
            // переводим наше изображение в байты
            byte[] inputBytes = GetBytes(foto);
            // создаём массив для итога с нужным размером
            byte[] outputBytes = new byte[inputBytes.Length];

            int kernelWidth = kernel.GetLength(0);
            int kernelHeight = kernel.GetLength(1);

            double sumR;
            double sumG;
            double sumB;
            double sumKernel;

            // проходим по изображению, не обрабатывая края
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    sumR = 0;
                    sumG = 0;
                    sumB = 0;
                    sumKernel = 0;

                    // проходим по ядру
                    for (int i = 0; i < kernelWidth; i++)
                    {
                        for (int j = 0; j < kernelHeight; j++)
                        {
                            int positionX = x + (i - (kernelWidth / 2));
                            int positionY = y + (j - (kernelHeight / 2));

                            // не обрабатываются края (при их категоричности доработаю)
                            if ((positionX < 0) || (positionX >= width) || (positionY < 0) || (positionY >= height))
                                continue;

                            // т.к. всё лежит подряд в массиве, то и умножаем позицию на 3, получаем 3 палитры подряд
                            byte r = inputBytes[3 * (width * positionY + positionX) + 0];
                            byte g = inputBytes[3 * (width * positionY + positionX) + 1];
                            byte b = inputBytes[3 * (width * positionY + positionX) + 2];

                            double kernelValue = kernel[i, j];

                            sumR += r * kernelValue;
                            sumG += g * kernelValue;
                            sumB += b * kernelValue;

                            sumKernel += kernelValue;
                        }
                    }

                    // Нельзя делить на ноль
                    if (sumKernel <= 0)
                        sumKernel = 1;

                    // Нельзя выйти за цветовые пределы
                    sumR = sumR / sumKernel;
                    if (sumR < 0)
                        sumR = 0;
                    if (sumR > 255)
                        sumR = 255;

                    sumG = sumG / sumKernel;
                    if (sumG < 0)
                        sumG = 0;
                    if (sumG > 255)
                        sumG = 255;

                    sumB = sumB / sumKernel;
                    if (sumB < 0)
                        sumB = 0;
                    if (sumB > 255)
                        sumB = 255;

                    // Записываем результат в цвет пикселя
                    outputBytes[3 * (width * y + x) + 0] = (byte)sumR;
                    outputBytes[3 * (width * y + x) + 1] = (byte)sumG;
                    outputBytes[3 * (width * y + x) + 2] = (byte)sumB;
                }
            }
            // Конвертируем полученные байты обратно в Битмап
            return GetBitmap(outputBytes, width, height);
        }

        ////////////////////////////////////////////////////////////////////////////
        // Два важных метода для конвертации
        // получение байтов из Битмапа
        public static byte[] GetBytes(Bitmap input)
        {
            int count = input.Height * input.Width * 3; // размер нашего изображения 
            BitmapData inputD = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb); // выделяем память
            var output = new byte[count];
            Marshal.Copy(inputD.Scan0, output, 0, count); // копируем себе в массив
            input.UnlockBits(inputD); // разблокировка памяти
            return output;
        }
        // получение Битмапа из байтов
        public static Bitmap GetBitmap(byte[] input, int width, int height)
        {
            if (input.Length % 3 != 0)
                return null;
            // проверяем сможем ли мы сконвертировать обратно (должно делиться на 3, так хранятся цветные пиксели)
            var output = new Bitmap(width, height);
            BitmapData outputD = output.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb); // выделяем память

            Marshal.Copy(input, 0, outputD.Scan0, input.Length);
            output.UnlockBits(outputD); // разблокировка памяти
            return output;
        }
        // Фильтр Гаусса
        public Bitmap Gauss()
        {
            //вычисляю свертку
            double sigma = 0.8;
            int W = 5;
 
            double[,] kernel = new double[W,W];
            double mean = W / 2;
            double sum = 0.0; 
            for (int x = 0; x < W; ++x)
                for (int y = 0; y < W; ++y)
                {
                    kernel[x,y] = Math.Exp(-0.5 * (Math.Pow((x - mean) / sigma, 2.0) + Math.Pow((y - mean) / sigma, 2.0)))
                                     / (2 * sigma * sigma);

                   
                    sum += kernel[x,y];
                }

            // нормализация
            for (int x = 0; x < W; ++x)
                for (int y = 0; y < W; ++y)
                    kernel[x,y] /= sum;

            // Использует определенные значения свёртки 
            //double[,] kernel = { { 0, 1, 0 }, { 1, 4, 1 }, { 0, 1, 0 } };
            CURRENTimage = Svertka(CURRENTimage, CURRENTimage.Height, CURRENTimage.Width, kernel);

                
            return CURRENTimage;
        }


        // Фильтр равномерный
        public Bitmap Evenly()
        {
            
            int W = 8;

            double[,] kernel1 = new double[W, W];
         
            // нормализация
            for (int x = 0; x < W; ++x)
                for (int y = 0; y < W; ++y)
                    kernel1[x, y] = 1.0/(W*W);

            // Использует определенные значения свёртки 
            //  double[,] kernel = { { 0, 3/ (W * W), 0 }, { 3 / (W * W), 0, 0 }, { 0, 3 / (W * W), 0 } };
            //double[,] kernel = { { 0, 3 / (W * W), 0 }, {0,  3 / (W * W), 0 }, { 0, 3 / (W * W), 0 } };
            CURRENTimage = Svertka(CURRENTimage, CURRENTimage.Height, CURRENTimage.Width, kernel1);
          //  calculateNewPixe(kernel);

            return CURRENTimage;
        }

        // Фильтр равномерный
        public Bitmap Res1(int k)
        {


            double[,]  kernel = new double[,] {
                { -k/8, -k/8, -k/8 },
                { -k/8, k+1, -k/8 },
                { -k/8, -k/8, -k/8 }
            };
            
            CURRENTimage = Svertka(CURRENTimage, CURRENTimage.Height, CURRENTimage.Width, kernel);
            //  calculateNewPixe(kernel);

            return CURRENTimage;
        }


        // Фильтр равномерный
        public Bitmap Res2()
        {
           

            int W = 8;

            double[,] kernel = new double[,]  {{ -1, -3, -4, -3 , -1},
{ -3,  0,  6,  0, -3},
 { -4,  6, 21,  6, -4 },
  { -3,  0,  6,  0, -3},
   { -1, -3, -4, -3, -1}
        };

            CURRENTimage = Svertka(CURRENTimage, CURRENTimage.Height, CURRENTimage.Width, kernel);
            //  calculateNewPixe(kernel);

            return CURRENTimage;
        }
        protected void calculateNewPixe(double[,] kernel)
        {
            
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    double resultR = 0;
                    double resultG = 0;
                    double resultB = 0;

                    for (int l = -radiusY; l <= radiusY; l++)
                    {
                        for (int k = -radiusX; k <= radiusX; k++)
                        {
                            // Значение по X
                            int idX = QM.clamp(x + k, 0, CURRENTimage.Width - 1);
                            // Значение по Y
                            int idY = QM.clamp(y + l, 0, CURRENTimage.Height - 1);

                            // Каналы в точки X,Y
                            Color h = CURRENTimage.GetPixel(idX, idY);


                            // kernel[0...radiusX, 0...radiusY]
                            // Двумерная свертка
                            resultR += h.R * kernel[k + radiusX, l + radiusY];
                            resultG += h.G * kernel[k + radiusX, l + radiusY];
                            resultB += h.B * kernel[k + radiusX, l + radiusY];
                        }
                    }
                    CURRENTimage.SetPixel(x, y, QM.Col(resultR, resultG, resultB));
                }
            }
           
        }
    
    public void changeContrast(double value)
        {
            // Изменение контрастности
            // Ну нет, это же не магические числа
            Bitmap helpMap = new Bitmap(_width, _height);
            double K = 0;
            int R = 0, G = 0, B = 0;

            if (value <= 0)
            { if (value == 0)
                    K = 1 ;
                else K = 1/ (-value);
            }
            else K = value;


                //Y=K*(Yold-Yav)+ Yav, 
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Color source2 = FIRSTimage.GetPixel(i, j);
                    Color result;
                    //  Color source2 = CURRENTimage.GetPixel(i, j);
                    if (i >= X1 && i < X2 && j >= Y1 && j < Y2)
                    {
                      
                        R = Convert.ToInt32(K * (source2.R - sR) + sR);
                        G = Convert.ToInt32(K * (source2.G - sG) + sG);
                        B = Convert.ToInt32(K * (source2.B - sB) + sB);

                        if (R < 0)
                            R = 0;
                        else
                        if (R > 255)
                            R = 255;
                        if (G < 0)
                            G = 0;
                        else
                     if (G > 255)
                            G = 255;

                        if (B < 0)
                            B = 0;
                        else
                     if (B > 255)
                            B = 255;
                        result = createColor(R, G, B);
                    }
                    else
                        result = createColor(source2.R, source2.G, source2.B);
                    helpMap.SetPixel(i, j, result);
                }
            }

            CURRENTimage = helpMap;
        }

        public void CalculateAverageRgb()
        {
            int rAvg = 0;
            int gAvg = 0;
            int bAvg = 0;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    rAvg += CURRENTimage.GetPixel(i, j).R;
                    gAvg += CURRENTimage.GetPixel(i, j).G;
                    bAvg += CURRENTimage.GetPixel(i, j).B;
                }
            }
            rAvg /= _width*_height;
            gAvg /= _width * _height;
            bAvg /= _width * _height;
            sR = rAvg;
            sG = gAvg;
            sB = bAvg;
        }

        public void makeBlackWhiteImage(int value)
        {
            // Получение черно-белого изображения

            Bitmap helpMap = new Bitmap(_width, _height);

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Color source = FIRSTimage.GetPixel(i, j);
                    if (i >= X1 && i < X2 && j >= Y1 && j < Y2)
                    {
                        if ((source.R + source.G + source.B) / 3 < value)
                        {
                            helpMap.SetPixel(i, j, createColor(0, 0, 0));
                        }
                        else
                        {
                            helpMap.SetPixel(i, j, createColor(255, 255, 255));
                        }
                    }
                    else helpMap.SetPixel(i, j, createColor(source.R, source.G, source.B));
                }
            }
            CURRENTimage = helpMap;
        }

        public void makeBlackWhiteImageOnly()
        {
            // Получение черно-белого изображения

            Bitmap helpMap = new Bitmap(_width, _height);
            Color result;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    Color source = FIRSTimage.GetPixel(i, j);
                    if (i >= X1 && i < X2 && j >= Y1 && j < Y2)
                    {
                        if ((source.R + source.G + source.B) < (127 * 3))
                        {
                            result = createColor(0, 0, 0);
                        }
                        else
                        {
                            result = createColor(255, 255, 255);
                        }
                    }
                    else result = createColor(source.R, source.G, source.B);
                    helpMap.SetPixel(i, j, result);
                }
            }
            CURRENTimage = helpMap;
        }
        //List< int> dop = CURRENTimag.GetPixel(i, j);

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    
    public void Steklo()
        {
            // Переаодит изображение а градации серого
            Bitmap helpMap = new Bitmap(_width, _height);
            Random rand = new Random();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {

                    helpMap.SetPixel(i, j, CURRENTimage.GetPixel(
                Clamp((int)(i + (rand.NextDouble() - 0.5) * 10), 0, CURRENTimage.Width - 1),
                Clamp((int)(j + (rand.NextDouble() - 0.5) * 10), 0, CURRENTimage.Height - 1)
                ));
                   
                }
            }
            CURRENTimage = helpMap;
        }

        public void grayScale()
        {
            // Переаодит изображение а градации серого
            Bitmap helpMap = new Bitmap(_width, _height);

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {


                    if (i >= X1 && i < X2 && j >= Y1 && j < Y2)
                    {
                        int brightness = getBrightPixel(i, j);
                        helpMap.SetPixel(i, j, Color.FromArgb(brightness, brightness, brightness));
                    } else helpMap.SetPixel(i, j, createColor(FIRSTimage.GetPixel(i, j).R, FIRSTimage.GetPixel(i, j).G, FIRSTimage.GetPixel(i, j).B));
                
                    }
            }
            CURRENTimage = helpMap;
        }

        public void negative()
        {
            // Переводит изображение а негатив

            Bitmap helpMap = new Bitmap(_width, _height);

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                     Color source = FIRSTimage.GetPixel(i, j);
                    // Color source = CURRENTimage.GetPixel(i, j);
                    if (i >= X1 && i < X2 && j >= Y1 && j < Y2)
                    {
                        helpMap.SetPixel(i, j, Color.FromArgb(BLACCOLOR - source.R, BLACCOLOR - source.G, BLACCOLOR - source.B));
                }
                    else helpMap.SetPixel(i, j, createColor(source.R, source.G, source.B));

            }
            }
            CURRENTimage = helpMap;

 

        }

        private Color createColor(int newR, int newG, int newB)
        {
            if (newR > BLACCOLOR)
                newR = BLACCOLOR;
            if (newR < WHITECOLOR)
                newR = WHITECOLOR;
            if (newG > BLACCOLOR)
                newG = BLACCOLOR;
            if (newG < WHITECOLOR)
                newG = WHITECOLOR;
            if (newB > BLACCOLOR)
                newB = BLACCOLOR;
            if (newB < WHITECOLOR)
                newB = WHITECOLOR;
            return Color.FromArgb(newR, newG, newB);
        }
        public void applyChanges()
        {
            FIRSTimage = CURRENTimage;
        }
        public void applyStartChanges()
        {
            STARTimage = CURRENTimage;
        }

        public void returnStart()
        {
            FIRSTimage = STARTimage;
            cancelChanges();
        }
        public void cancelChanges()
        {
            CURRENTimage = FIRSTimage;
        }
        public void newOblast(int x1, int x2, int y1, int y2)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
        }

        public void oldOblast()
        {
            X1 = 0;
            X2 = _width;
            Y1 = 0;
            Y2 = _height;
        }
    }

// Quick Math
static class QM
{
    private const int iMIN = 0;
    private const int iMAX = 255;

    public static int clamp(int value, int min = iMIN, int max = iMAX)
    {
        return Math.Min(Math.Max(min, value), max); ;
    }

    public static byte clamp(int value)
    {
        return (byte)Math.Min(Math.Max(iMIN, value), iMAX); ;
    }

    public static byte clamp(double value)
    {
        return (byte)Math.Min(Math.Max(iMIN, value), iMAX);
    }
    public static byte clamp(double value, double min = iMIN, double max = iMAX)
    {
        return (byte)Math.Min(Math.Max(min, value), max);
    }
    public static Color Col(double ch)
    {
        ch = clamp(ch, 0, 255);
        return Color.FromArgb((byte)ch, (byte)ch, (byte)ch);
    }
    public static Color Col(int ch)
    {
        ch = clamp(ch, 0, 255);
        return Color.FromArgb(ch, ch, ch);
    }
    public static Color Col(double R, double G, double B)
    {
        return Color.FromArgb(clamp(R), clamp(G), clamp(B));
    }
    public static Color Col(int R, int G, int B)
    {
        return Color.FromArgb(clamp(R), clamp(G), clamp(B));
    }
    public static Color Col(byte ch)
    {
        return Color.FromArgb(ch, ch, ch);
    }
}
}
