//using System;
//using System.Collections;
//using System.Configuration;
//using Lemon.Lib.Docruzer.VO;
//using ATLCRXLib;

///// <summary>
///** 부가기능 API.
//* @author KONAN Technology
//* @since 2010.06.15
//* @version 1.0
//* Copyright ⓒ Konan Technology. All Right Reserved
//* ==================================================
//* 변경자/변경일 : 장진후 / 2012.07.20
//* 변경사유/내역 : 표준소스 2.0 버전용 변경(setConnection, 변수 변경)
//* 변경자/변경일 : 전한길 / 2011.06.15
//* 변경사유/내역 : .NET 버전 신규 개발.(AKC, PPK, KRE 3가지만 구현되어 있음)
//*/
///// </summary>

//namespace com.konantech.search.module.AddOnModule
//{
//    public class AddOnModule
//    {
//        public AddOnModule()
//        {
//        }

//        public string INDEX_CHARSET = "EUCKR";//ConfigurationManager.AppSettings["INDEX_CHARSET"];
//        private string addr = "61.109.247.88:8577";//ConfigurationManager.AppSettings["MODULE_SERVICE_ADDRESS"];
//        private string id = "administrator";
//        private string pw = "konan";
//        private string etc = "";

//        /** 인기검색어 메소드 (getPopularKwd).
//         * @param maxResultCount 최대 결과 수
//         * @param language 언어
//         * @param charset 문자셋
//         * @param domainNo 모듈 도메인 번호
//         * @return 결과값(String[])
//         * @throws IOException
//         * @throws Exception
//         * @throws KonanException
//         */
//        public String[,] getPopularKwd(int maxResultCount, int language, int charset, int domainNo)
//        {
//            object eleCnt = 0;
//            object[] keyword = null;

//            string[,] arr_ppk = null;                                   //인기검색어 배열

//            string request_name = "POPULAR_KEYWORD";                    //자동분류기 request 이름
//            string request_family = "PPK";                              //자동분류기 request family

//            object Language = language;                                 // 언어셋
//            object domain_no = domainNo;                                // 도메인번호
//            object Charset = charset;                                   //EUCKR , UTF8 두가지 중 선택 
//            object max_result_count = maxResultCount;                   // 최대 보여줄 수 있는 인기검색어 수

//            ClientClass crx = new ClientClass();  //CrxClient 객체 생성

//            long hd = crx.connect(addr, id, pw, etc);                   //핸들 생성

//            try
//            {
//                if (hd < 0)
//                    throw new Exception(crx.msg);

//                //request param 설정 -  rc 체크는 별도 수행 안함
//                crx.clear_request();
//                crx.put_request_family(request_family);
//                crx.put_request_name(request_name);

//                //파라미터 설정
//                crx.put_request_param("DOMAIN_NO", "INT32", ref domain_no);
//                crx.put_request_param("LANGUAGE", "INT32", ref Language);
//                crx.put_request_param("CHARSET", "INT32", ref Charset);
//                crx.put_request_param("MAX_RESULT_COUNT", "INT32", ref max_result_count);

//                //핸들, 파라미터명, 파라미터타입, 파라미터값
//                if (crx.submit_request() < 0)
//                {  //request  생성
//                    throw new Exception(crx.msg);
//                }
//                if (crx.receive_response() < 0)
//                {  //response 얻음
//                    throw new Exception(crx.msg);
//                }

//                object paramCnt = 0;
//                crx.get_response_param_count(out paramCnt);

//                for (int i = 0; i < (int)paramCnt; i++)
//                {
//                    object paramName = "";
//                    crx.get_response_param_name(out paramName, i);

//                    //KEYWORD_COUNT
//                    if (paramName.Equals("KEYWORD_COUNT"))
//                    {
//                        crx.get_response_param_value(out eleCnt, i);
//                        arr_ppk = new String[(int)eleCnt, 2];

//                    }
//                    //KEYWORD
//                    if (paramName.Equals("KEYWORD"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";


//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_ppk[j, 0] = keyword[j].ToString();

//                    }
//                    //TAG
//                    if (paramName.Equals("TAG"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";

//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_ppk[j, 1] = keyword[j].ToString();
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                err.ToString();
//            }
//            finally
//            {
//                if (crx != null) crx.disconnect();    //핸들 제거
//            }

//            return arr_ppk;
//        }


//        /** 검색어 자동완성 메소드 (getCompleteKwd).
//         * @param keyword 키워드
//         * @param maxResultCount 최대 결과 수
//         * @param flag 결과 형식 플래그 (앞, 뒤 단어 일치 여부)
//         * @param language 언어
//         * @param charset 문자셋
//         * @param domainNo 모듈 도메인 번호
//         * @return 결과값(String[])
//         * @throws IOException
//         * @throws Exception
//         * @throws KonanException
//         */

//        public String[,] getCompleteKwd(String seed, int maxResultCount, int flag, int language, int charset, int domainNo)
//        {
//            object eleCnt = 0;
//            object[] keyword = null;

//            string[,] arr_akc = null;                                       //인기검색어 배열

//            string request_name = "COMPLETE_KEYWORD";                       //자동분류기 request 이름
//            string request_family = "AKC";                                  //자동분류기 request family

//            object Language = language;                                     // 언어셋
//            object domain_no = domainNo;                                    // 도메인번호
//            object Charset = charset;                                       //EUCKR , UTF8 두가지 중 선택 
//            object max_result_count = maxResultCount;                       // 최대 보여줄 수 있는 자동완성 수
//            object Flag = flag;
//            object Seed = seed;

//            ClientClass crx = new ClientClass();      //CrxClient 객체 생성
            
//            long hd = crx.connect(addr, id, pw, etc); //핸들 생성

//            try
//            {
//                if (hd < 0)
//                    throw new Exception(crx.msg);

//                //request param 설정 -  rc 체크는 별도 수행 안함
//                crx.clear_request();
//                crx.put_request_family(request_family);
//                crx.put_request_name(request_name);

//                //파라미터 설정

//                crx.put_request_param("DOMAIN_NO", "INT32", ref domain_no);
//                crx.put_request_param("SEED", "CHAR", ref Seed);
//                crx.put_request_param("FLAG", "INT32", ref Flag);
//                crx.put_request_param("LANGUAGE", "INT32", ref Language);
//                crx.put_request_param("CHARSET", "INT32", ref Charset);
//                crx.put_request_param("MAX_RESULT_COUNT", "INT32", ref max_result_count);

//                //핸들, 파라미터명, 파라미터타입, 파라미터값
//                if (crx.submit_request() < 0)
//                {  //request  생성
//                    throw new Exception(crx.msg);
//                }
//                if (crx.receive_response() < 0)
//                {  //response 얻음
//                    throw new Exception(crx.msg);
//                }

//                object paramCnt = 0;
//                crx.get_response_param_count(out paramCnt);

//                for (int i = 0; i < (int)paramCnt; i++)
//                {
//                    object paramName = "";
//                    crx.get_response_param_name(out paramName, i);

//                    if (paramName.Equals("KEYWORD_COUNT"))
//                    {
//                        crx.get_response_param_value(out eleCnt, i);
//                        arr_akc = new String[(int)eleCnt, 4];

//                    }
//                    // KEYWORD
//                    if (paramName.Equals("KEYWORD"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";


//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_akc[j, 0] = keyword[j].ToString();

//                    }
//                    // RANK
//                    /*if (paramName.Equals("RANK"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";

//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_akc[j, 1] = keyword[j].ToString();
//                    }*/
//                    // TAG
//                    if (paramName.Equals("TAG"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";

//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_akc[j, 1] = keyword[j].ToString();
//                    }
//                    // NUM
//                    if (paramName.Equals("NUM"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";

//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_akc[j, 2] = keyword[j].ToString();
//                    }
//                    // CONVERTED_KEYWORD
//                    if (paramName.Equals("CONVERTED_KEYWORD"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";

//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_akc[j, 3] = keyword[j].ToString();
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                err.ToString();
//            }
//            finally
//            {
//                if (crx != null) crx.disconnect();    //핸들 제거
//            }

//            return arr_akc;
//        }

//        /** 추천어 메소드 (getRecommandKwd).
//         * @param keyword 키워드
//         * @param maxResultCount 최대 결과 수
//         * @param language 언어
//         * @param charset 문자셋
//         * @param domainNo 모듈 도메인 번호
//         * @return 결과값(String[])
//         * @throws IOException
//         * @throws Exception
//         * @throws KonanException
//         */
//        public String[] getRecommandKwd(String inputWord, int maxResultCount, int language, int charset, int domainNo)
//        {
//            object eleCnt = 0;
//            object[] keyword = null;

//            string[] arr_kre = null;                                    //추천검색어 배열

//            string request_name = "RECOMMEND_KEYWORD";                  //자동분류기 request 이름
//            string request_family = "KRE";                              //자동분류기 request family

//            object Language = language;                                 // 언어셋
//            object domain_no = domainNo;                                // 도메인번호
//            object Charset = charset;                                   // EUCKR , UTF8 두가지 중 선택 
//            object max_result_count = maxResultCount;                   // 최대 보여줄 수 있는 추천검색어 수
//            object input_word = inputWord;                              // 입력 단어

//            ClientClass crx = new ClientClass();  //CrxClient 객체 생성

//            long hd = crx.connect(addr, id, pw, etc);                   //핸들 생성

//            try
//            {
//                if (hd < 0)
//                    throw new Exception(crx.msg);

//                //request param 설정 -  rc 체크는 별도 수행 안함
//                crx.clear_request();
//                crx.put_request_family(request_family);     //request family
//                crx.put_request_name(request_name);         //request 이름

//                //파라미터 설정
//                crx.put_request_param("DOMAIN_NO", "INT32", ref domain_no);
//                crx.put_request_param("LANGUAGE", "INT32", ref Language);
//                crx.put_request_param("CHARSET", "INT32", ref Charset);
//                crx.put_request_param("MAX_RESULT_COUNT", "INT32", ref max_result_count);
//                crx.put_request_param("INPUT_WORD", "CHAR", ref input_word);

//                //핸들, 파라미터명, 파라미터타입, 파라미터값
//                if (crx.submit_request() < 0)
//                {  //request  생성
//                    throw new Exception(crx.msg);
//                }
//                if (crx.receive_response() < 0)
//                {  //response 얻음
//                    throw new Exception(crx.msg);
//                }

//                object paramCnt = 0;
//                crx.get_response_param_count(out paramCnt);

//                for (int i = 0; i < (int)paramCnt; i++)
//                {
//                    object paramName = "";
//                    crx.get_response_param_name(out paramName, i);

//                    // KEYWORD COUNT
//                    if (paramName.Equals("KEYWORD_COUNT"))
//                    {
//                        crx.get_response_param_value(out eleCnt, i);
//                        arr_kre = new String[(int)eleCnt];

//                    }
//                    // KEYWORD
//                    if (paramName.Equals("KEYWORD"))
//                    {
//                        crx.get_response_param_element_count(out eleCnt, i);
//                        object src = "";


//                        crx.get_response_param_value(out src, i);
//                        keyword = (object[])src;

//                        for (int j = 0; j < (int)eleCnt; j++)
//                            arr_kre[j] = keyword[j].ToString();
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                err.ToString();
//            }
//            finally
//            {
//                if (crx != null) crx.disconnect();    //핸들 제거
//            }

//            return arr_kre;
//        }
//    }
//}