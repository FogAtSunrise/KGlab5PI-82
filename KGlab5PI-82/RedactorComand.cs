using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private int _width;
        private int _height;
        private string nameFile;
        private Bitmap rawImage; 
        private Dictionary<int, int> tableBrightnessForGistogramm;
        readonly int COUNCOLOR = 256;
        readonly int BLACCOLOR = 255;
        readonly int WHITECOLOR = 0;
        private Bitmap STARTimage { get; set; } //исходное изображение
        private Bitmap FIRSTimage { get; set; } //исходное изображение
        public Bitmap CURRENTimage { get; set; } // преобразованное

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
                  //  Color source2 = CURRENTimage.GetPixel(i, j);
                    Color source2 = FIRSTimage.GetPixel(i, j);
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
                    Color result = createColor(R, G, B);
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
                    if ((source.R + source.G + source.B) / 3 < value)
                    {
                        helpMap.SetPixel(i, j, createColor(0, 0, 0));
                    }
                    else
                    {
                        helpMap.SetPixel(i, j, createColor(255, 255, 255));
                    }
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
                    if ((source.R + source.G + source.B)  < (127*3))
                    {
                       result = createColor(0,0,0);
                                      }
                    else
                    {
                        result = createColor(255, 255, 255);
                    } 
                    
                    helpMap.SetPixel(i, j, result);
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
                    CURRENTimage = FIRSTimage;
                    int brightness = getBrightPixel(i, j);
                    helpMap.SetPixel(i, j, Color.FromArgb(brightness, brightness, brightness));
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
                    helpMap.SetPixel(i, j, Color.FromArgb(BLACCOLOR - source.R, BLACCOLOR - source.G, BLACCOLOR - source.B));
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
    }
}
