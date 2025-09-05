using FluentValidation;
using MediatR;
using SimpleClinicApi.DataAccess.Repositories;
using SimpleClinicApi.Infrastructure;
using SimpleClinicApi.Infrastructure.Auth.Commands;
using SimpleClinicApi.Infrastructure.Auth.Utilities;

namespace SimpleClinicApi.Extensions;

public static class ServicesExtensions
{
    public static void AddClinicServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommand>()
        );
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(DbContextTransactionPipelineBehavior<,>)
        );
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommand>();

        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IProcedureRepository, ProcedureRepository>();
        services.AddScoped<IMedicationRepository, MedicationRepository>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(RegisterUserCommand).Assembly);

            cfg.LicenseKey =
                "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OT"
                + "kwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVub"
                + "nlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg3MDExMjAwIi"
                + "wiaWF0IjoiMTc1NTUwNTc4MSIsImFjY291bnRfaWQiOiIwMTk4YmM0YzA4ZWQ3YmQwOGVmOGI4YmQ1NzB"
                + "kMmU2OCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazJ5NHJ0a3llY3drMDdrOWVqeGJlZHBnIiwic3ViX2lk"
                + "IjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.xwcVhgiv7XXQf-45DcC4jqU-kV51QqjlSd7Lhwf"
                + "xGRz-V_GEZP6M-6DpYuKuuy_-KyoI8RNCNqtYR9X_SM8ExIHu2CQpEF7XImfU6nWuMH6ZJ5Nd4SiBA_mn"
                + "oZhQ6fZ_JL5VDb2Di8c4Hme9ltz0ZL-k8yWMtGvfrWMrH-D_19ifl1d-DNKmPB82U9izCU89tzcmRIkBu"
                + "o7WH2S3SEBTAgbBTzofTW-I9EjuxyXtxReRTw1_Bsc3r6zPKfPvtbA8EcsGmiR-D8fWT0PmYl2VS1GuCiY"
                + "C-JFTUkeo3mwHtTUrtmeZ93dpc_CbftPj4D7LB9pzJDnMnl9nnB00yxZauw";
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}
