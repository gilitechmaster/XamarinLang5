using lang6.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace lang6
{
    //Page1은 문서 입력 화면
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename)) //문자열이 널인지 공백인지 확인한다.
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                File.WriteAllText(note.Filename, note.Text);
            }
            await Navigation.PopAsync();
        }//저장버튼 누르면

        async void OnCalc1ButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }

            else
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(note.Filename, note.Text);
                StreamWriter sw = new StreamWriter(filename, true);
                string[] arr = note.Text.Split(new string[]{

                "",

                        }, StringSplitOptions.None);


                foreach (string B in arr)
                {

                    if (B.Contains("\n")  // replace로 2차처리
                        )
                        sw.WriteLine("{0}", B
                          .Replace("\n", "")
                          );

                }

                sw.Close();

            }


            await Navigation.PopAsync();
        }
        //조합버튼 누르면
        //웹에서 문서를 수집하면, 어수선한 글이 많아서
        //조합을 먼저한 이후에 분해한다.

        async void OnCalc2ButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }

            else
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(note.Filename, note.Text);
                StreamWriter sw = new StreamWriter(filename, true);
                string[] arr = note.Text.Split(new string[]{

                    ". ",//순차처리하므로 ". "를 앞에해두어야 한다.
                    ".",
                    ", ",
                    ",",
                    "? ",
                    "?",
                    "! ",
                    "!",
                    " "


                        }, StringSplitOptions.None);
                int Key = 1;

                sw.WriteLine("====자연어처리==== \n");

                //동사+접속사 처리에 중요점은
                //접속사의 끝자만 남기고 시작, 중간자는
                //동사에 흡수를 하게 되는데

                //반대로 동사는 시작자를 남기고
                //끝자는 접속사에 흡수된다.

                //둘러싸고 = 둘러싸[다] + [그리]고

                foreach (string B in arr)
                {
                    sw.WriteLine("\n" + Key++ + ") " + B);
                    //접속사의 가나다 순으로 배열한다.


                    //ㄱ
                    if (B.EndsWith("으나") == true)
                        sw.WriteLine("{0}", B
                            .Replace("으나", "다 + 그러나(으나_분리)")
                            );
                    if (B.EndsWith("고") == true)
                        sw.WriteLine("{0}", B

                            .Replace("이라고", "이고")
                            .Replace("이고", "이다 + 그리고(고_분리)")

                            //이라고 - 이고 - 이다 그리고
                            //순차처리는 여전히 적용해야 한다.

                            .Replace("했다고", "했고")
                            .Replace("했고", "했다 + 그리고(고_분리)")
                            
                            .Replace("시켰고", "시켰다 + 그리고(고_분리)")
                            .Replace("났다고", "났다 + 그리고(고_분리)")

                            .Replace("루고", "루다 + 그리고(고_분리)")
                            .Replace("하고", "하다 + 그리고(고_분리)")
                            .Replace("크고", "크다 + 그리고(고_분리)")
                            .Replace("되고", "되다 + 그리고(고_분리)")
                            .Replace("르고", "르다 + 그리고(고_분리)")
                            .Replace("있고", "있다 + 그리고(고_분리)")
                            .Replace("넓고", "넓다 + 그리고(고_분리)")
                            .Replace("싸고", "싸다 + 그리고(고_분리)")
                            .Replace("덮고", "덮다 + 그리고(고_분리)")
                            .Replace("얇고", "얇다 + 그리고(고_분리)")
                            .Replace("받고", "받다 + 그리고(고_분리)")
                            .Replace("놓고", "놓다 + 그리고(고_분리)")
                            .Replace("없고", "없다 + 그리고(고_분리)")
                            .Replace("내고", "내다 + 그리고(고_분리)")
                            );

                    if (B.EndsWith("면") == true)
                        sw.WriteLine("{0}", B
                            .Replace("되면", "되다 + 그러면(면_분리)")
                            .Replace("들면", "들다 + 그러면(면_분리)")
                            .Replace("보면", "보다 + 그러면(면_분리)")
                            .Replace("루면", "루다 + 그러면(면_분리)")
                            .Replace("누면", "누다 + 그러면(면_분리)")
                            .Replace("추면", "추다 + 그러면(면_분리)")
                            );

                    if (B.EndsWith("서") == true)
                        sw.WriteLine("{0}", B
                            .Replace("가서", "가다 + 그래서(서_분리)")
                            //.Replace("에서", "이다 + 그래서(서_분리)")
                            //"서"는 맞으나 그래서로 연결되지 않는다.
                            //문장이 이어지지 않으면 분리하지 않는다.
                            );

                    if (B.EndsWith("서") == true)
                        sw.WriteLine("{0}", B
                            .Replace("면서", "다 + 그러면서(서_분리)")
                            .Replace("삶아서", "삶다 + 그러면서(서_분리)")
                            );

                    if (B.EndsWith("게") == true)
                        sw.WriteLine("{0}", B
                            //보이게 된다 = 보이다
                            //보이다 + 그렇게 + 된다
                            //[된다]는 객체가 없는 동사이다.

                            //크게 방해된다 = 크다 + 그렇게 방해된다
                            //[크다] [방해되다]라는 동사 2개모두
                            //객체가 존재한다.

                            .Replace("하게", "하다 + 그렇게(게_분리)")
                            .Replace("깊게", "깊다 + 그렇게(게_분리)")
                            .Replace("이게", "이다 + 그렇게(게_분리)")
                            .Replace("크게", "크다 + 그렇게(게_분리)")
                            .Replace("없게", "없다 + 그렇게(게_분리)")
                            .Replace("르게", "르다 + 그렇게(게_분리)")
                            );

                    if (B.EndsWith("도") == true)
                        sw.WriteLine("{0}", B
                            .Replace("맞서기도", "맞서다 + 그렇기도(도_분리)")
                            );

                    if (B.EndsWith("는") == true)
                        sw.WriteLine("{0}", B
                            .Replace("나서는", "나서다 + 그러는(는_분리)")
                            .Replace("하다는", "하다 + 그러는(는_분리)")
                            .Replace("키는", "키다 + 그러는(는_분리)")
                            .Replace("하는", "하다 + 그러는(는_분리)")
                            .Replace("있는", "있다 + 그러는(는_분리)")
                            .Replace("없는", "없다 + 그러는(는_분리)")
                            .Replace("이는", "이다 + 그러는(는_분리)")
                            .Replace("가는", "가다 + 그러는(는_분리)")
                            );

                    if (B.EndsWith("데") == true)
                        sw.WriteLine("{0}", B
                            .Replace("는데", "다 + 그런데(데_분리)")
                            );
                    //ㄷ

                    if (B.EndsWith("는") == true)
                        sw.WriteLine("{0}", B
                            .Replace("바꾸는", "바꾸다 + 또는(는_분리)")
                            .Replace("갖는", "갖다 + 또는(는_분리)")

                            .Replace("하다는", "다는")
                            .Replace("없다는", "다는")
                            .Replace("다는", "다 + 또는(는_분리)")
                            );

                    


                    //ㄹ

                    if (B.EndsWith("록") == true)//왠지 이거 아닐거같은데
                        sw.WriteLine("{0}", B
                            .Replace("하도록", "하다 + 비록(록_분리)")
                            );

                    if (B.EndsWith("고") == true)
                        sw.WriteLine("{0}", B
                            .Replace("다고", "다 + 라고(고_분리)")
                            );

                    //ㅎ
                    if (B.EndsWith("지만") == true)
                        sw.WriteLine("{0}", B
                            .Replace("지만", "다 + 하지만(지만_분리)")
                            );

                }
                sw.Close();

            }


            await Navigation.PopAsync();
        }//분해버튼 누르면



        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }
            await Navigation.PopAsync();
        }//삭제버튼 누르면
    }
}
